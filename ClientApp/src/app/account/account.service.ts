import {Injectable} from '@angular/core';
import {environment} from 'src/environments/environment';
import {User} from '../shared/models/user';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {ReplaySubject} from 'rxjs';
import {map, catchError} from 'rxjs/operators';
import {Router} from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<User | null>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) {
  }

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        }
      }),
      catchError(error => {
        console.error('Login error', error);
        throw error;
      })
    );
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      map(user => {
        if (user) {
          this.setCurrentUser(user);
        }
      }),
      catchError(error => {
        console.error('Register error', error);
        throw error;
      })
    );
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  confirmEmail(userName: string) {
    const token = this.getTokenFromLocalStorage();
    return this.http.post<boolean>(this.baseUrl + 'account/confirm-email', {userName, token}).pipe(
      catchError(error => {
        console.error('Confirm email error', error);
        throw error;
      })
    );
  }

  resetPassword(userName: string, newPassword: string) {
    const token = this.getTokenFromLocalStorage();
    return this.http.post<boolean>(this.baseUrl + 'account/reset-password', {userName, token, newPassword}).pipe(
      catchError(error => {
        console.error('Reset password error', error);
        throw error;
      })
    );
  }

  changePassword(userName: string, currentPassword: string, newPassword: string) {
    const token = this.getTokenFromLocalStorage();
    return this.http.post<boolean>(this.baseUrl + 'account/change-password', {
      userName,
      currentPassword,
      newPassword,
      token
    }).pipe(
      catchError(error => {
        console.error('Change password error', error);
        throw error;
      })
    );
  }

  checkEmailExists(email: string) {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.getTokenFromLocalStorage()}`);
    return this.http.get<boolean>(this.baseUrl + 'account/check-email-exists?email=' + email, { headers }).pipe(
      catchError(error => {
        console.error('Check email exists error', error);
        throw error;
      })
    );
  }

  checkUsernameExists(username: string) {
    return this.http.get<boolean>(this.baseUrl + 'account/check-username-exists?username=' + username).pipe(
      catchError(error => {
        console.error('Check username exists error', error);
        throw error;
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

  getDecodedToken(token: string) {
    return JSON.parse(atob(token.split('.')[1]));
  }

  private getTokenFromLocalStorage() {
    const user = JSON.parse(localStorage.getItem('user') || '{}');
    return user.token;
  }
}