import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Order, OrderUpdate } from '../shared/models/order';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { OrderToReturn } from '../shared/models/order-to-return';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getOrdersForUser(): Observable<OrderToReturn[]> {
    return this.http.get<OrderToReturn[]>(this.baseUrl + 'orders');
  }

  getOrderDetailed(id: string): Observable<OrderToReturn> {
    return this.http.get<OrderToReturn>(this.baseUrl + 'orders/' + id);
  }

  getOrdersByUserEmail(userName: string): Observable<OrderToReturn[]> {
    return this.http.get<OrderToReturn[]>(`${this.baseUrl}orders/user-orders`, {
      params: { userName }
    });
  }

  updateUserOrder(orderId: string, orderUpdate: OrderUpdate) {
    return this.http.put(`${this.baseUrl}orders/${orderId}`, orderUpdate);
  }

  getAllOrders(): Observable<OrderToReturn[]> {
    return this.http.get<OrderToReturn[]>(this.baseUrl + 'orders/all');
  }
}
