import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { OrdersHistoryService } from '../orders-history.service';
import { ActivatedRoute } from '@angular/router';
import { OrderHistoryToReturn } from 'src/app/shared/models/order-history-to-return';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: false,
  selector: 'app-orders-history',
  templateUrl: './orders-history.component.html',
  styleUrls: ['./orders-history.component.sass']
})
export class OrdersHistoryComponent implements OnInit {

  orderHistories: OrderHistoryToReturn[] = [];
  displayedColumns: string[] = ['order', 'date', 'total', 'status'];

  constructor(
    private ordersHistoryService: OrdersHistoryService,
    private route: ActivatedRoute,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadOrderHistories();
  }

  loadOrderHistories() {
    this.ordersHistoryService.getOrderHistoriesByUserId().subscribe({
      next: (orderHistories) => {
        this.orderHistories = orderHistories;
        this.cdr.markForCheck();
      },
      error: (error) => console.error('Error loading orders:', error)
    });
  }
}
