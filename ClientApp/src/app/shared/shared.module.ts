import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MaterialModule} from './modules/material/material.module';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {RouterModule} from '@angular/router';
import { BasketSummaryComponent } from './basket-summary/basket-summary.component';
import { OrderTotalsComponent } from './order-totals/order-totals.component';

@NgModule({
  declarations: [
    BasketSummaryComponent,
    OrderTotalsComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule,
  ],
  exports: [
    MaterialModule,
    BasketSummaryComponent,
    OrderTotalsComponent
  ]
})
export class SharedModule {
}
