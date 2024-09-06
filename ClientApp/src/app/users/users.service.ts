import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from '../shared/models/user';
import { HttpClient } from '@angular/common/http';
import { AccountService } from '../account/account.service';
import {map, Observable, of, switchMap, take} from 'rxjs';
import { Address } from '../shared/models/address';
import { UserPhoto } from '../shared/models/user-photo';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  baseUrl = environment.apiUrl;
  users: User[] = [];
  memberCache = new Map();
  user: User | undefined;

  constructor(private http: HttpClient, private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) {
          this.user = user;
        }
      }
    })
  }

  getUser(username: string) {
    const member = [...this.memberCache.values()]
      .reduce((arr, elem) => arr.concat(elem.result), [])
      .find((member: User) => member.username === username);

    if (member) return of(member);

    return this.http.get<User>(`${this.baseUrl}users/username/${username}`);
  }

  getUserById(id: string): Observable<User> {
    return this.http.get<User>(`${this.baseUrl}users/${id}`);
  }

  getUserByEmail(email: string): Observable<User> {
    return this.http.get<User>(`${this.baseUrl}users/email/${email}`);
  }

  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.baseUrl}users`);
  }

  searchUsersByName(name: string): Observable<User[]> {
    return this.http.get<User[]>(`${this.baseUrl}users/search/${name}`);
  }

  getUserAddress(): Observable<Address> {
    return this.http.get<Address>(`${this.baseUrl}users/address`);
  }

  updateUserAddress(address: Address): Observable<Address> {
    return this.http.put<Address>(`${this.baseUrl}users/address`, address);
  }
  
  setMainUserPhoto(userPhotoId: string): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}users/photo/main/${userPhotoId}`, {});
  }

  deleteUserPhoto(userPhotoId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}users/photo/${userPhotoId}`);
  }
}
