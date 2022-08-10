import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IUser } from 'src/app/models/user';
import { IUserCreate } from 'src/app/models/userCreate';
import { ApiService } from 'src/app/services/api.service';
import { CustomValidators } from 'src/app/validators/validators';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent implements OnInit {

  currentUser!: IUser;
  updateViewModel = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.minLength(8), CustomValidators.forbiddenPassword()]),
    confirmedPassword: new FormControl('', []),
    firstName: new FormControl('', [Validators.required]),
    lastName: new FormControl('', [Validators.required]),
  },
    CustomValidators.passwordMatch('password', 'confirmedPassword')
  );

  errorMessage!:string;

  constructor(private api: ApiService, private router: Router) { }

  ngOnInit(): void {
    this.api.getCurrentUser().subscribe(x => {
      this.currentUser = x;
      this.updateViewModel.controls['email'].setValue(this.currentUser.email);
      this.updateViewModel.controls['firstName'].setValue(this.currentUser.firstName);
      this.updateViewModel.controls['lastName'].setValue(this.currentUser.lastName);
    });
  }

  onSubmit(): void {
    if (this.updateViewModel.valid) {
      let user: IUserCreate = {
        email: this.updateViewModel.get('email')?.value,
        password: this.updateViewModel.get('password')?.value,
        confirmedPassword: '', // SA NU UITI AICI
        firstName: this.updateViewModel.get('firstName')?.value,
        lastName: this.updateViewModel.get('lastName')?.value
      };

      this.api.updateUser(user).subscribe(res => this.router.navigate(['profile']), err => console.log(err.error));
    }
  }
}
