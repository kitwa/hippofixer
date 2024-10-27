import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { Router } from '@angular/router';
import { City } from '../_models/city';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<User | null>(1); // Initialize with `User | null`
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) {
    // Check localStorage for a user on initialization
    const user = JSON.parse(localStorage.getItem('user'));
    if (user) {
      this.setCurrentUser(user);
    } else {
      this.currentUserSource.next(null); // Emit null if no user is stored
    }
  }

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  resetPassword(model: any) {
    return this.http.post(this.baseUrl + 'account/reset-password/' + model.email, {});
  }

  newPassword(model: any) {
    return this.http.post(this.baseUrl + 'account/new-password', model);
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      map((user: User) => {
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  setCurrentUser(user: User) {
    user.roles = [];
    const roles = this.getDecodedToken(user.token).role;
    Array.isArray(roles) ? user.roles = roles : user.roles.push(roles);
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null); // Emit null on logout
    this.router.navigateByUrl('/login');
  }

  getDecodedToken(token: string) {
    return JSON.parse(atob(token.split('.')[1]));
  }

  getCities() {
    return this.http.get<City[]>(this.baseUrl + 'account/cities');
  }
}
