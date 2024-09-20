import { NgModule } from '@angular/core';
import {RouterModule, Routes } from '@angular/router';
import { WishlistComponent } from './wishlist/wishlist.component';

const routes : Routes = [
  {path: '', component: WishlistComponent}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ], exports : [
    RouterModule
  ]
})
export class WishlistRoutingModulee { }
