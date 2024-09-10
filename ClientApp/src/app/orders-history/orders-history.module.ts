import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { OrdersHistoryComponent } from './orders-history/orders-history.component';
import { OrdersHistoryRoutingModule } from './orders-history-routing.module';



@NgModule({
  declarations: [
    OrdersHistoryComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    OrdersHistoryRoutingModule
  ]
})
export class OrdersHistoryModule { }
