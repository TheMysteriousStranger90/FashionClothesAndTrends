import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Order, OrderUpdateDto } from '../shared/models/order';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getOrdersForUser() {
    return this.http.get<Order[]>(this.baseUrl + 'orders');
  }
  getOrderDetailed(id: number) {
    return this.http.get<Order>(this.baseUrl + 'orders/' + id);
  }


  getOrdersByUserEmail(userName: string): Observable<Order[]> {
    return this.http.get<Order[]>(`${this.baseUrl}orders/user-orders`, {
      params: { userName }
    });
  }

  updateUserOrder(orderId: string, orderUpdateDto: OrderUpdateDto): Observable<Order> {
    return this.http.put<Order>(`${this.baseUrl}orders/${orderId}`, orderUpdateDto);
  }
}
