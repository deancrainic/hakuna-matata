import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService {

  constructor(public acc: AccountService, public router: Router) { }

  canActivate(): boolean {
    if (!this.acc.checkLoginStatus()) {
      this.router.navigate(['login']);
      return false;
    }
    
    return true;
  }
}
