import { Component, OnInit } from '@angular/core';
import { OrderHistory } from 'src/app/shared/models/order-history';
import { OrdersHistoryService } from '../orders-history.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-orders-history',
  templateUrl: './orders-history.component.html',
  styleUrls: ['./orders-history.component.sass']
})
export class OrdersHistoryComponent implements OnInit {

  orderHistories: OrderHistory[] = [];
  displayedColumns: string[] = ['order', 'date', 'total', 'status'];

  constructor(private ordersHistoryService: OrdersHistoryService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    const userId = this.route.snapshot.paramMap.get('userId');
    if (userId) {
      this.getOrderHistoriesForUser(userId);
    }
  }

  getOrderHistoriesForUser(userId: string) {
    this.ordersHistoryService.getOrderHistoriesForUser(userId).subscribe({
      next: orderHistories => this.orderHistories = orderHistories
    });
  }
}
