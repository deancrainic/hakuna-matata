import { DatePipe } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router'
import { Methods } from 'src/app/methods/methods';
import { DateRange } from 'src/app/models/dateRange';
import { IReservation } from 'src/app/models/reservation';
import { IReservationProperty } from 'src/app/models/reservationProperty';
import { IReservationUpdate } from 'src/app/models/reservationUpdate';
import { ApiService } from 'src/app/services/api.service';
import { CustomValidators } from 'src/app/validators/validators';

@Component({
  selector: 'app-edit-reservation',
  templateUrl: './edit-reservation.component.html',
  styleUrls: ['./edit-reservation.component.css']
})
export class EditReservationComponent implements OnInit {

  message!: string;
  reserveErrorMessage!: string;
  reservation!: IReservation;
  @Input()
  reservationId!: number;
  property!: IReservationProperty;

  range!: FormGroup;
  reservationForm!: FormGroup;

  unavailableDays!: DateRange[];
  availableDays!:any;

  constructor(
    private api: ApiService, 
    private router: Router, 
    private date: DatePipe,
    public dialogRef: MatDialogRef<EditReservationComponent>
  ) { }

  formatDate(d: Date): Date {
    return Methods.formatDate(d);
  }

  ngOnInit(): void {
    this.api.getReservationById(this.reservationId).subscribe(res => {
      this.reservation = res,
      this.range = new FormGroup({
        start: new FormControl(this.formatDate(res.checkinDate), [Validators.required]),
        end: new FormControl(this.formatDate(res.checkoutDate), [Validators.required]),
      }, CustomValidators.reservationLength('start', 'end'));
      this.reservationForm = new FormGroup({
        range: this.range,
        guests: new FormControl('', [Validators.required, CustomValidators.guestsNumber(res.property.maxGuests)])
      });
      this.reservationForm.controls['guests'].setValue(res.guestsNumber);
      this.property = res.property;
      
      this.api.getReservationDatesByPropertyId(res.property.propertyId).subscribe(x => {
        this.unavailableDays = x;
        this.availableDays = (d: Date): boolean => {
          let valid = true;

          let currentDate = new Date();
          currentDate.setDate(currentDate.getDate() - 1);

          if (this.formatDate(d) < this.formatDate(currentDate)) {
            valid = false;
          }

          if (this.unavailableDays.length > 0) {
            this.unavailableDays.forEach(dr => {
              let checkin = new Date(dr.checkinDate);
              checkin.setDate(checkin.getDate() + 1);
              let checkout = new Date(dr.checkoutDate);
              if (this.formatDate(d) > this.formatDate(checkin) && 
                  this.formatDate(d) < this.formatDate(checkout) && 
                  (this.formatDate(d) < this.formatDate(this.reservation.checkinDate) ||
                  this.formatDate(d) > this.formatDate(this.reservation.checkoutDate))) {
                valid = false;
              }
            });
          }
          
          return valid;
        };
      });
    }, err => this.message = err.error);
  }

  goToProperty(propertyId: number) {
    this.router.navigate(['property', propertyId]);
    this.closeModal();
  }

  getTotalPrice(price: number): number {
    const milisecondsPerDay = 1000 * 60 * 60 * 24;     
    
    return price * Math.floor((this.reservationForm.get('range.end')?.value - this.reservationForm.get('range.start')?.value) / milisecondsPerDay)
  }

  updateReservation() {
    let res: IReservationUpdate = {
      checkinDate: this.date.transform(this.reservationForm.get('range.start')?.value, 'yyyy-MM-dd'),
      checkoutDate: this.date.transform(this.reservationForm.get('range.end')?.value, 'yyyy-MM-dd'),
      guestsNumber: this.reservationForm.get('guests')?.value
    }

    this.api.updateReservation(this.reservation.reservationId, res).subscribe(res => this.closeModal(), err => this.reserveErrorMessage = err.error);
  }

  closeModal() {
    this.dialogRef.close();
  }
}
