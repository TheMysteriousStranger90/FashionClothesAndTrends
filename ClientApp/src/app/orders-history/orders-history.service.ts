import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { OrderHistory } from '../shared/models/order-history';

@Injectable({
  providedIn: 'root'
})
export class OrdersHistoryService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getOrderHistoriesForUser(userId: string): Observable<OrderHistory[]> {
    return this.http.get<OrderHistory[]>(`${this.baseUrl}orderhistory/${userId}`);
  }

  getOrderHistoryById(id: string): Observable<OrderHistory> {
    return this.http.get<OrderHistory>(`${this.baseUrl}orderhistory/order/${id}`);
  }
}
