import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {authGuard} from './core/guards/auth.guard';
import {HomeComponent} from './home/home.component';
import {FavoritesComponent} from './favorites/favorites.component';

const routes: Routes = [
  {path: 'home', component: HomeComponent, data: {breadcrumb: 'Home'}},
  {
    path: 'shop', loadChildren: () => import('./shop/shop.module').then(m => m.ShopModule),
    data: {breadcrumb: 'Shop'}
  },
  {
    path: 'account',
    loadChildren: () => import('./account/account.module').then(m => m.AccountModule),
    data: {breadcrumb: {skip: true}}
  },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [authGuard],
    children: [
      {
        path: 'basket', loadChildren: () => import('./basket/basket.module').then(m => m.BasketModule),
        data: {breadcrumb: 'Basket'}
      },
      {
        path: 'checkout',
        loadChildren: () => import('./checkout/checkout.module').then(m => m.CheckoutModule),
        data: {breadcrumb: 'Checkout'}
      },
      {
        path: 'favorites',
        component: FavoritesComponent
      },
      {
        path: 'wishlist',
        loadChildren: () => import('./wishlist/wishlist.module').then(m => m.WishlistModule)
      },
      {
        path: 'orders',
        loadChildren: () => import('./orders/orders.module').then(m => m.OrdersModule)
      },
      {
        path: 'orders-history',
        loadChildren: () => import('./orders-history/orders-history.module').then(m => m.OrdersHistoryModule)
      },
    ]
  },
  {path: '**', redirectTo: '', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
