import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WishlistComponent } from './wishlist/wishlist.component';
import { WishlistRoutingModulee } from './wishlist-routing.module';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    WishlistComponent
  ],
  imports: [
    CommonModule,
    WishlistRoutingModulee,
    SharedModule,
  ]
})
export class WishlistModule { }
