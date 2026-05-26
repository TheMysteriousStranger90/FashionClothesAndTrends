import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { OrderToReturn } from 'src/app/shared/models/order-to-return';
import { OrdersService } from '../orders.service';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: false,
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.sass']
})
export class OrdersComponent implements OnInit {

  orders: OrderToReturn[] = [];
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
      },
      error: (error) => console.error('Error loading orders:', error)
    });
  }
}
