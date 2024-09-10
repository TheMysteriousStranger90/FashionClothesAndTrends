import {Component, OnInit} from '@angular/core';
import {Order} from 'src/app/shared/models/order';
import {OrdersService} from '../orders.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.sass']
})
export class OrdersComponent implements OnInit {

  orders: Order[] = [];
  displayedColumns: string[] = ['order', 'date', 'total', 'status'];
  constructor(private orderService: OrdersService) {
  }

  ngOnInit(): void {
    this.getOrders();
  }

  getOrders() {
    this.orderService.getOrdersForUser().subscribe({
      next: orders => this.orders = orders
    })
  }

}
