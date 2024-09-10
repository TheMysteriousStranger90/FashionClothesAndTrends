import {NgModule} from "@angular/core";
import {RouterModule, Routes} from "@angular/router";
import { OrdersHistoryComponent } from "./orders-history/orders-history.component";


const routes: Routes = [
  {path: '', component: OrdersHistoryComponent},
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class OrdersHistoryRoutingModule {
}