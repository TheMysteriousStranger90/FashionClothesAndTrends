import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {ShopComponent} from './shop/shop.component';
import {ClothingDetailsComponent} from './clothing-details/clothing-details.component';

const routes: Routes = [
  {path: '', component: ShopComponent},
  {path: ':id', component: ClothingDetailsComponent, data: {breadcrumb: {alias: 'clothingDetails'}}},
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class ShopRoutingModule {
}
