import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ShopComponent} from './shop/shop.component';
import {ClothingItemComponent} from './clothing-item/clothing-item.component';
import {ClothingDetailsComponent} from './clothing-details/clothing-details.component';
import {SharedModule} from '../shared/shared.module';
import {ShopRoutingModule} from './shop-routing.module';

@NgModule({
  declarations: [
    ShopComponent,
    ClothingItemComponent,
    ClothingDetailsComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ShopRoutingModule
  ]
})
export class ShopModule {
}
