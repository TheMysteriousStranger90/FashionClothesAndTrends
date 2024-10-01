import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {CoreModule} from '../core/core.module';
import {SharedModule} from '../shared/shared.module';
import {AdminRoutingModule} from './admin-routing.module';
import {AdminPanelComponent} from './admin-panel/admin-panel.component';
import {UserManagementComponent} from './user-management/user-management.component';
import { CreateCouponComponent } from './create-coupon/create-coupon.component';
import { ApplyCouponComponent } from './apply-coupon/apply-coupon.component';
import { CreateBrandComponent } from './create-brand/create-brand.component';
import { CreateClothingItemComponent } from './create-clothing-item/create-clothing-item.component';
import { AddPhotoToClothingItemComponent } from './add-photo-to-clothing-item/add-photo-to-clothing-item.component';


@NgModule({
  declarations: [
    AdminPanelComponent,
    UserManagementComponent,
    CreateCouponComponent,
    ApplyCouponComponent,
    CreateBrandComponent,
    CreateClothingItemComponent,
    AddPhotoToClothingItemComponent
  ],
  imports: [
    CommonModule,
    CoreModule,
    SharedModule,
    AdminRoutingModule,
  ]
})
export class AdminModule {
}
