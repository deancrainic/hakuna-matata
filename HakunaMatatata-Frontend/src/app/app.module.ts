import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavbarComponent } from './components/navbar/navbar.component';
import { SearchFormComponent } from './components/search-form/search-form.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from './material.module';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { PropertiesListComponent } from './components/properties-list/properties-list.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { PropertyDetailsComponent } from './components/property-details/property-details.component';

import { ApiService } from './services/api.service';
import { ReservationDetailsTrasporterService } from './services/reservation-details-trasporter.service';
import { AccountService } from './services/account.service';
import { AuthInterceptor } from './services/auth.interceptor';
import { DatePipe } from '@angular/common';
import { ProfileComponent } from './components/profile/profile.component';
import { EditProfileComponent } from './components/edit-profile/edit-profile.component';
import { PropertyComponent } from './components/property/property.component';
import { ReservationsComponent } from './components/reservations/reservations.component';
import { EditReservationComponent } from './components/edit-reservation/edit-reservation.component';
import { CustomValidators } from './validators/validators';
import { ViewImagesComponent } from './components/view-images/view-images.component';
import { PropertyReservationsComponent } from './components/property-reservations/property-reservations.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    SearchFormComponent,
    LoginComponent,
    RegisterComponent,
    PropertiesListComponent,
    PropertyDetailsComponent,
    ProfileComponent,
    EditProfileComponent,
    PropertyComponent,
    ReservationsComponent,
    EditReservationComponent,
    ViewImagesComponent,
    PropertyReservationsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    HttpClientModule
  ],
  providers: [
    ApiService, 
    ReservationDetailsTrasporterService, 
    AccountService, 
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    DatePipe,
    CustomValidators
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
