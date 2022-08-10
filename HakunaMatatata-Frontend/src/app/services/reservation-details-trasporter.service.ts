import { Injectable } from '@angular/core';
import { dateInputsHaveChanged } from '@angular/material/datepicker/datepicker-input-base';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReservationDetailsTrasporterService {
  
  startDateSource = new BehaviorSubject(new Date(new Date().setHours(0, 0, 0, 0)));
  endDateSource = new BehaviorSubject(new Date(new Date().setHours(48, 0, 0, 0)));
  guestsSource = new BehaviorSubject(1);

  currentStartDate = this.startDateSource.asObservable();
  currentEndDate = this.endDateSource.asObservable();
  currentGuests = this.guestsSource.asObservable();

  constructor() {}

  changeStartDate(startDate: Date): void {
    this.startDateSource.next(startDate);
  }

  changeEndDate(endDate: Date): void {
    this.endDateSource.next(endDate);
  }

  changeGuests(guests: number): void {
    this.guestsSource.next(guests);
  }
}
