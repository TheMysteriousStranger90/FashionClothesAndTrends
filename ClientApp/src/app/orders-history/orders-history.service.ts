import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {environment} from 'src/environments/environment';
import {OrderHistoryToReturn} from '../shared/models/order-history-to-return';

@Injectable({
  providedIn: 'root'
})
export class OrdersHistoryService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  getOrderHistoriesByUserId(): Observable<OrderHistoryToReturn[]> {
    return this.http.get<OrderHistoryToReturn[]>(`${this.baseUrl}ordershistory/user`);
  }

  getOrderHistoryById(id: string): Observable<OrderHistoryToReturn> {
    return this.http.get<OrderHistoryToReturn>(`${this.baseUrl}ordershistory/order/${id}`);
  }
}
