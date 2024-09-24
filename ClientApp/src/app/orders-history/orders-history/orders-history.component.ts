import { Component, OnInit } from '@angular/core';
import { OrdersHistoryService } from '../orders-history.service';
import { ActivatedRoute } from '@angular/router';
import { OrderHistoryToReturn } from 'src/app/shared/models/order-history-to-return';

@Component({
  selector: 'app-orders-history',
  templateUrl: './orders-history.component.html',
  styleUrls: ['./orders-history.component.sass']
})
export class OrdersHistoryComponent implements OnInit {

  orderHistories: OrderHistoryToReturn[] = [];
  displayedColumns: string[] = ['order', 'date', 'total', 'status'];

  constructor(private ordersHistoryService: OrdersHistoryService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadOrderHistories();
  }
  loadOrderHistories() {
    this.ordersHistoryService.getOrderHistoriesByUserId().subscribe({
      next: (orderHistories) => {
        this.orderHistories = orderHistories;
        this.orderHistories.forEach(item => {
        });
      },
      error: (error) => console.error('Error loading orders:', error)
    });
  }
}
