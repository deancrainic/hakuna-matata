import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { IUserCreate } from '../models/userCreate';
import { IUserCredentials } from '../models/userCredentials';


@Injectable({
  providedIn: 'root'
})
export class AccountService {

  loginStatus = new BehaviorSubject<boolean>(this.checkLoginStatus());
  userId = new BehaviorSubject<any>(localStorage.getItem('userId'));
  payload!: any;

  constructor(private http: HttpClient, private router: Router) { }

  register(user: IUserCreate): Observable<IUserCreate> {
    return this.http.post<IUserCreate>("https://localhost:7154/api/Users", user);
  }

  login(userCredentials: IUserCredentials): Observable<string> {
    return this.http.post<string>("https://localhost:7154/api/Users/login", userCredentials).pipe(
      map(result => {
        if (result) {
          this.loginStatus.next(true);
          localStorage.setItem('loginStatus', '1');
          localStorage.setItem('token', result);

          this.payload = JSON.parse(atob(result.split('.')[1]));          
          localStorage.setItem('userId', this.payload["Id"]);
          localStorage.setItem('exp', this.payload['exp']);
          this.userId.next(localStorage.getItem('userId'));
        }

        return result;
      })
    );
  }

  logout(): void {
    this.loginStatus.next(false);
    localStorage.removeItem('token');
    localStorage.removeItem('userId');
    localStorage.removeItem('exp');
    localStorage.setItem('loginStatus', '0');
  }

  checkLoginStatus(): boolean {
    let loginCookie = localStorage.getItem('loginStatus');

    if (loginCookie == '1') {
      if (localStorage.getItem('token') === null || localStorage.getItem('token') === undefined) {
        localStorage.removeItem('token');
        localStorage.removeItem('userId');
        localStorage.removeItem('exp');
        localStorage.setItem('loginStatus', '0');
        return false;
      }

      let exp = Number.parseInt(<string>localStorage.getItem('exp'));

      if (exp === null || exp === undefined) {
        localStorage.removeItem('token');
        localStorage.removeItem('userId');
        localStorage.removeItem('exp');
        localStorage.setItem('loginStatus', '0');
        return false;
      }

      if (exp > (new Date()).getTime() / 1000) {
        return true;
      }

      localStorage.removeItem('token');
      localStorage.removeItem('userId');
      localStorage.removeItem('exp');
      localStorage.setItem('loginStatus', '0');
      return false;
    }

    return false;
  }

  get isLoggedIn() {
    return this.loginStatus.asObservable();
  }

  get currentUserId() {
    return this.userId.asObservable();
  }
}
