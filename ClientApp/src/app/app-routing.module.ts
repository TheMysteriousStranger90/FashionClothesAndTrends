import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {authGuard} from './core/guards/auth.guard';
import {HomeComponent} from './home/home.component';
import {FavoritesComponent} from './favorites/favorites.component';
import { NotificationsComponent } from './notifications/notifications.component';
import { adminGuard } from './core/guards/admin.guard';
import { UserEditorComponent } from './users/user-editor/user-editor.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  {
    path: 'shop',
    loadChildren: () => import('./shop/shop.module').then(m => m.ShopModule)
  },
  {
    path: 'account',
    loadChildren: () => import('./account/account.module').then(m => m.AccountModule)
  },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [authGuard],
    children: [
      {
        path: 'basket',
        loadChildren: () => import('./basket/basket.module').then(m => m.BasketModule)
      },
      {
        path: 'checkout',
        loadChildren: () => import('./checkout/checkout.module').then(m => m.CheckoutModule)
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
        path: 'notifications',
        component: NotificationsComponent
      },
      {
        path: 'orders',
        loadChildren: () => import('./orders/orders.module').then(m => m.OrdersModule)
      },
      {
        path: 'orders-history',
        loadChildren: () => import('./orders-history/orders-history.module').then(m => m.OrdersHistoryModule)
      },
      { path: 'user-editor', component: UserEditorComponent },
      {
        path: 'admin',
        canActivate: [adminGuard],
        loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule)
      }
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }