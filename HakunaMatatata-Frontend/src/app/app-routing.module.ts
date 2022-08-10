import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EditProfileComponent } from './components/edit-profile/edit-profile.component';
import { EditReservationComponent } from './components/edit-reservation/edit-reservation.component';
import { LoginComponent } from './components/login/login.component';
import { ProfileComponent } from './components/profile/profile.component';
import { PropertyDetailsComponent } from './components/property-details/property-details.component';
import { PropertyComponent } from './components/property/property.component';
import { RegisterComponent } from './components/register/register.component';
import { ReservationsComponent } from './components/reservations/reservations.component';
import { SearchFormComponent } from './components/search-form/search-form.component';
import { AuthGuardService } from './services/auth-guard.service';

const routes: Routes = [{
  path: '',
  component: SearchFormComponent
}, {
  path: 'login',
  component: LoginComponent
}, {
  path: 'register',
  component: RegisterComponent
}, {
  path: 'property/:id',
  component: PropertyDetailsComponent
}, {
  path: 'profile',
  component: ProfileComponent,
  canActivate: [AuthGuardService],
  children: [{
    path: 'edit',
    component: EditProfileComponent
  }, {
    path: 'property',
    component: PropertyComponent
  }, {
    path: 'reservations',
    component: ReservationsComponent
  }]
}, {
  path: '**', redirectTo: ''
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
