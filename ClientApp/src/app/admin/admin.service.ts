import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from '../shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  getUsersWithRoles() {
    return this.http.get<User[]>(this.baseUrl + 'admin/users-with-roles');
  }

  updateUserRoles(username: string, roles: string[]) {
    return this.http.post<string[]>(this.baseUrl + 'admin/edit-roles/'
      + username + '?roles=' + roles, {});
  }

  getRoles() {
    return this.http.get<string[]>(this.baseUrl + 'admin/roles');
  }

  addRole(roleName: string) {
    return this.http.post(this.baseUrl + 'admin/add-role', { roleName });
  }

  deleteRole(roleName: string) {
    return this.http.delete(this.baseUrl + 'admin/delete-role/' + roleName);
  }
}
