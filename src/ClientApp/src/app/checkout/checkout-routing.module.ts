import {RouterModule, Routes} from "@angular/router";
import {CheckoutComponent} from "./checkout/checkout.component";
import {CheckoutSuccessComponent} from "./checkout-success/checkout-success.component";
import {NgModule} from "@angular/core";

const routes: Routes = [
  {path: '', component: CheckoutComponent},
  {path: 'success', component: CheckoutSuccessComponent},
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class CheckoutRoutingModule {
}