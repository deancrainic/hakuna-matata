import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CustomValidators } from 'src/app/validators/validators';
import { IUserCreate } from '../../models/userCreate';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerViewModel = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.minLength(8), CustomValidators.forbiddenPassword()]),
    confirmedPassword: new FormControl('', []),
    firstName: new FormControl('', [Validators.required]),
    lastName: new FormControl('', [Validators.required]),
  }, 
    CustomValidators.passwordMatch('password', 'confirmedPassword')
  );

  errorMessage!:string;

  constructor(private acc: AccountService, private router: Router) { }

  ngOnInit(): void {
  }

  goToLogin(): void {
    this.router.navigate(['/login']);
  }

  register(): void {
    if (this.registerViewModel.valid) {
      let user: IUserCreate = {
        email: this.registerViewModel.get('email')?.value,
        password: this.registerViewModel.get('password')?.value,
        confirmedPassword: this.registerViewModel.get('confirmedPassword')?.value,
        firstName: this.registerViewModel.get('firstName')?.value,
        lastName: this.registerViewModel.get('lastName')?.value,
      };
    
      this.acc.register(user).subscribe(() => this.goToLogin(), err => this.errorMessage = err.error);
    } 
  }
}

