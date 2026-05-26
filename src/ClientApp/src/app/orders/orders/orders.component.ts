import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { Order } from 'src/app/shared/models/order';
import { OrdersService } from '../orders.service';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: false,
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.sass']
})
export class OrdersComponent implements OnInit {

  orders: Order[] = [];
  displayedColumns: string[] = ['order', 'date', 'total', 'status'];

  constructor(private orderService: OrdersService, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.getOrders();
  }

  getOrders() {
    this.orderService.getOrdersForUser().subscribe({
      next: orders => {
        this.orders = orders;
        this.cdr.markForCheck();
      }
    });
  }
}
