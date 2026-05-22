import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AdminPanelComponent} from './admin-panel/admin-panel.component';
import {UserManagementComponent} from './user-management/user-management.component';
import {CreateCouponComponent} from './create-coupon/create-coupon.component';
import {ApplyCouponComponent} from './apply-coupon/apply-coupon.component';
import { CreateBrandComponent } from './create-brand/create-brand.component';
import { CreateClothingItemComponent } from './create-clothing-item/create-clothing-item.component';
import { AddPhotoToClothingItemComponent } from './add-photo-to-clothing-item/add-photo-to-clothing-item.component';
import { EditOrderComponent } from './edit-order/edit-order.component';


const routes: Routes = [
  {path: 'admin-panel', component: AdminPanelComponent},
  {path: 'user-management', component: UserManagementComponent},
  {path: 'create-coupon', component: CreateCouponComponent},
  {path: 'apply-coupon', component: ApplyCouponComponent},
  {path: 'create-brand', component: CreateBrandComponent},
  {path: 'create-clothing-item', component: CreateClothingItemComponent},
  {path: 'add-photo-to-clothing-item', component: AddPhotoToClothingItemComponent},
  {path: 'edit-order', component: EditOrderComponent},
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class AdminRoutingModule {
}
