import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Methods } from 'src/app/methods/methods';
import { CustomValidators } from 'src/app/validators/validators';
import { IProperty } from '../../models/property';
import { ApiService } from '../../services/api.service';
import { ReservationDetailsTrasporterService } from '../../services/reservation-details-trasporter.service';

@Component({
  selector: 'app-search-form',
  templateUrl: './search-form.component.html',
  styleUrls: ['./search-form.component.css']
})
export class SearchFormComponent implements OnInit {

  range = new FormGroup({
    start: new FormControl(''),
    end: new FormControl(''),
  }, CustomValidators.reservationLength('start', 'end'));

  searchViewModel = new FormGroup({
    location: new FormControl(''),
    range: this.range,
    guests: new FormControl('')
  });
 
  properties!: IProperty[];
  shownProperties!: IProperty[];
  selectedOption = 0;

  options = [
    { name: 'Lowest price', value: 0 },
    { name: 'Highest price', value: 1 },
    { name: 'Newest', value: 2 },
    { name: 'Oldest', value: 3 }
  ];

  availableDays!:any;

  constructor(private api: ApiService, private transporter: ReservationDetailsTrasporterService) { }

  ngOnInit(): void {
    this.api.getAllProperties().subscribe(x => this.properties = x);
    this.availableDays = (d: Date): boolean => {
      let valid = true;

      let currentDate = new Date();
      currentDate.setDate(currentDate.getDate() - 1);

      if (Methods.formatDate(d) < Methods.formatDate(currentDate)) {
        valid = false;
      }

      return valid;
    };
  }

  onSubmit(): void {    
    this.api.getAllPropertiesSorted(this.selectedOption).subscribe(
      res => {
        this.shownProperties = res.filter(
          p => p.address.toLowerCase().includes(this.searchViewModel.get('location')?.value.toLowerCase()) 
                && p.maxGuests >= this.searchViewModel.get('guests')?.value);
        if (this.searchViewModel.get('range.start')?.value === '') {
          this.transporter.changeStartDate(new Date(new Date().setHours(0, 0, 0, 0)));
        } else {
          this.transporter.changeStartDate(this.searchViewModel.get('range.start')?.value);
        }
    
        if (this.searchViewModel.get('range.end')?.value === '') {
          this.transporter.changeEndDate(new Date(new Date().setHours(48, 0, 0, 0)));
        } else {
          this.transporter.changeEndDate(this.searchViewModel.get('range.end')?.value);
        }
    
        if (this.searchViewModel.get('guests')?.value === '') {
          this.transporter.changeGuests(1);
        } else {
          this.transporter.changeGuests(this.searchViewModel.get('guests')?.value);
        }
      }
    );
  }
}