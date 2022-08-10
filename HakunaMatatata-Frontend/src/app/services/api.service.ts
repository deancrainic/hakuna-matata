import { HttpClient, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DateRange } from '../models/dateRange';
import { IProperty } from '../models/property';
import { IPropertyCreate } from '../models/propertyCreate';
import { IPropertyReservation } from '../models/propertyReservation';
import { IReservation } from '../models/reservation';
import { IReservationCreate } from '../models/reservationCreate';
import { IReservationUpdate } from '../models/reservationUpdate';
import { IUser } from '../models/user';
import { IUserCreate } from '../models/userCreate';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  baseUrl = "https://localhost:7154";
  constructor(private http: HttpClient) { }

  getCurrentUser(): Observable<IUser> {
    return this.http.get<IUser>(this.baseUrl + '/api/Users/current');
  }

  updateUser(user: IUserCreate): Observable<IUser> {
    return this.http.put<IUser>(this.baseUrl + '/api/Users/current', user);
  }

  getAllProperties(): Observable<IProperty[]> {
    return this.http.get<IProperty[]>(this.baseUrl + '/api/Properties');
  }

  getAllPropertiesSorted(sortType: number): Observable<IProperty[]> {
    return this.http.get<IProperty[]>(`${this.baseUrl}/api/Properties/sorted/${sortType}`);
  }

  getPropertyById(id: number): Observable<IProperty> {
    return this.http.get<IProperty>(`${this.baseUrl}/api/Properties/${id}`);
  }

  addProperty(property: IPropertyCreate): Observable<IProperty> {
    return this.http.post<IProperty>(this.baseUrl + '/api/Properties/current', property);
  }

  deleteProperty(): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/api/Properties/current`);
  }

  updateProperty(property: IPropertyCreate): Observable<IProperty> {
    return this.http.put<IProperty>(`${this.baseUrl}/api/Properties/current`, property);
  }

  uploadImage(formData: FormData): Observable<HttpEvent<any>> {
    return this.http.post<HttpEvent<any>>(`${this.baseUrl}/api/Images`, formData, {reportProgress: true, observe: 'events'});
  }

  deleteImage(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/api/Images/${id}`);
  }

  makeReservation(reservation: IReservationCreate): Observable<IReservation> {
    return this.http.post<IReservation>(`${this.baseUrl}/api/Reservations/current`, reservation);
  }

  deleteReservation(reservationId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/api/Reservations/current/${reservationId}`);
  }

  getReservations(): Observable<IReservation[]> {
    return this.http.get<IReservation[]>(this.baseUrl + '/api/Reservations/current');
  }

  getReservationById(reservationId: number): Observable<IReservation> {
    return this.http.get<IReservation>(`${this.baseUrl}/api/Reservations/current/${reservationId}`)
  }

  getReservationDatesByPropertyId(propertyId: number): Observable<DateRange[]> {
    return this.http.get<DateRange[]>(`${this.baseUrl}/api/Reservations/property/${propertyId}`)
  }

  getReservationsByPropertyId(propertyId: number): Observable<IPropertyReservation[]> {
    return this.http.get<IPropertyReservation[]>(`${this.baseUrl}/api/Reservations/property/${propertyId}/reservations`);
  }

  updateReservation(reservationId: number, reservation: IReservationUpdate): Observable<IReservation> {
    return this.http.put<IReservation>(`${this.baseUrl}/api/Reservations/current/${reservationId}`, reservation);
  }

  deleteReservationByOwner(reservationId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/api/Reservations/property/${reservationId}`);
  }
}
