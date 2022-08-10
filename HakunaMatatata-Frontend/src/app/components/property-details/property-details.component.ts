import { AfterViewInit, Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { Subscription } from 'rxjs/internal/Subscription';
import { IReservationCreate } from 'src/app/models/reservationCreate';
import { AccountService } from 'src/app/services/account.service';
import { IProperty } from '../../models/property';
import { ApiService } from '../../services/api.service';
import { ReservationDetailsTrasporterService } from '../../services/reservation-details-trasporter.service';
import { DatePipe } from '@angular/common';
import { Observable } from 'rxjs';
import { DateRange } from 'src/app/models/dateRange';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';
import { ViewImagesComponent } from '../view-images/view-images.component';
import { CustomValidators } from 'src/app/validators/validators';
import { Methods } from 'src/app/methods/methods';

@Component({
  selector: 'app-property-details',
  templateUrl: './property-details.component.html',
  styleUrls: ['./property-details.component.css']
})
export class PropertyDetailsComponent implements OnInit {

  loginStatus!: boolean;
  propertyId!: number;
  property!: Observable<IProperty>;

  startDate!: Date;
  endDate!: Date;
  guests!: number;

  range!: FormGroup;
  reservationForm!: FormGroup;

  subscription1!: Subscription;
  subscription2!: Subscription;
  subscription3!: Subscription;

  errorMessage!: string;
  unavailableDays!: DateRange[];
  availableDays!: any;

  dialogConfig = new MatDialogConfig();
  modalDialog!: MatDialogRef<ViewImagesComponent, any>;

  constructor(
    private api: ApiService, 
    private activeRoute: ActivatedRoute, 
    private router: Router, 
    private transporter: ReservationDetailsTrasporterService, 
    private acc: AccountService,
    private date: DatePipe,
    private matDialog: MatDialog) { }

  formatDate(d: Date): Date {
    return Methods.formatDate(d);
  }

  ngOnInit(): void {
    this.activeRoute.params.subscribe(params => this.propertyId = params['id']);
    this.property = this.api.getPropertyById(this.propertyId);
    
    this.property.subscribe(res => {
      this.range = new FormGroup({
        start: new FormControl(new Date(new Date().setHours(0, 0, 0, 0)), [Validators.required]),
        end: new FormControl(new Date(new Date().setHours(48, 0, 0, 0)), [Validators.required]),
      }, CustomValidators.reservationLength('start', 'end'));

      this.reservationForm = new FormGroup({
        range: this.range,
        guests: new FormControl(this.guests, [Validators.required, CustomValidators.guestsNumber(res.maxGuests)])
      });
      
      this.api.getReservationDatesByPropertyId(res.propertyId).subscribe(x => {
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
                  this.formatDate(d) < this.formatDate(checkout)) {
                valid = false;
              }
            });
          }
          
          return valid;
        };
        this.subscription1 = this.transporter.currentEndDate.subscribe(d => this.endDate = d);
        this.subscription2 = this.transporter.currentStartDate.subscribe(d => this.startDate = d);
        this.subscription3 = this.transporter.currentGuests.subscribe(g => this.guests = g);

        this.reservationForm.get('guests')?.setValue(this.guests);
        this.transporter.changeStartDate(this.startDate);
        this.transporter.changeEndDate(this.endDate);
        this.transporter.changeGuests(this.guests);

        this.reservationForm.get('range.start')?.setValue(this.startDate);
        this.reservationForm.get('range.end')?.setValue(this.endDate);
        this.reservationForm.get('guests')?.setValue(this.guests);
      });
      
      this.acc.isLoggedIn.subscribe(x => this.loginStatus = x);
    });
  }

  ngOnDestroy(): void {
    this.subscription1.unsubscribe();
    this.subscription2.unsubscribe();
    this.subscription3.unsubscribe();
  }
  
  getTotalPrice(price: number): number {
    const milisecondsPerDay = 1000 * 60 * 60 * 24;
    
    return price * Math.floor((this.reservationForm.get('range.end')?.value - this.reservationForm.get('range.start')?.value) / milisecondsPerDay)
  }

  makeReservation(): void {
    const reservation: IReservationCreate = {
      propertyId: this.propertyId,
      checkinDate: this.date.transform(this.reservationForm.get('range.start')?.value, 'yyyy-MM-dd'),
      checkoutDate: this.date.transform(this.reservationForm.get('range.end')?.value, 'yyyy-MM-dd'),
      guestsNumber: this.reservationForm.get('guests')?.value
    };

    if (this.loginStatus == true) {
      
      this.api.makeReservation(reservation).subscribe(
        res => {
          this.router.navigate(['profile', 'reservations']);
        },
        err => this.errorMessage = err.error);
    } else {
      this.router.navigate(['login']);
    }
  }

  openImagesModal() {
    this.property.subscribe(res => {
      this.dialogConfig.id = "projects-modal-component";
      this.dialogConfig.height = "80%"
      this.dialogConfig.width = "80%";
      this.modalDialog = this.matDialog.open(ViewImagesComponent, this.dialogConfig);
      this.modalDialog.componentInstance.property = res;
    });
  }

  viewImages(): void {
    this.openImagesModal();
  }
}
