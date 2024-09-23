import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {CheckoutComponent} from './checkout/checkout.component';
import {CheckoutAddressComponent} from './checkout-address/checkout-address.component';
import {CheckoutDeliveryComponent} from './checkout-delivery/checkout-delivery.component';
import {CheckoutPaymentComponent} from './checkout-payment/checkout-payment.component';
import {CheckoutReviewComponent} from './checkout-review/checkout-review.component';
import {CheckoutSuccessComponent} from './checkout-success/checkout-success.component';
import {SharedModule} from '../shared/shared.module';
import {CheckoutRoutingModule} from './checkout-routing.module';

@NgModule({
  declarations: [
    CheckoutComponent,
    CheckoutAddressComponent,
    CheckoutDeliveryComponent,
    CheckoutPaymentComponent,
    CheckoutReviewComponent,
    CheckoutSuccessComponent
  ],
  imports: [
    CommonModule,
    CheckoutRoutingModule,
    SharedModule
  ]
})
export class CheckoutModule {
}