import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  loginStatus!: Observable<boolean>;
  userId!: Observable<string>;

  constructor(private acc: AccountService) { }

  ngOnInit(): void {
    this.loginStatus = this.acc.isLoggedIn;
    this.userId = this.acc.userId;
  }

  onLogout(): void {
    this.acc.logout();
  }
}
