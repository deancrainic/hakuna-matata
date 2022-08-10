import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginViewModel = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required])
  });

  errorMessage!: string;

  constructor(private acc: AccountService, private router: Router) { }

  ngOnInit(): void {
  }

  login(): void {
    if (this.loginViewModel.valid) {
      this.acc.login(this.loginViewModel.value).subscribe(
        () => this.router.navigate(['/']),
        err => this.errorMessage = err.error);
    }
  }
}