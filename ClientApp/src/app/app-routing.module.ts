import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

const routes: Routes = [

  {path: 'basket', loadChildren: () => import('./basket/basket.module').then(m => m.BasketModule),
    data: {breadcrumb: 'Basket'}
  },

  {
    path: 'orders',
    canActivate: [authGuard],
    loadChildren: () => import('./orders/orders.module').then(m => m.OrdersModule)
  },
  {
    path: 'orderhistory',
    canActivate: [authGuard],
    loadChildren: () => import('./orders-history/orders-history.module').then(m => m.OrdersHistoryModule)
  },
  {
    path: 'account',
    loadChildren: () => import('./account/account.module').then(m => m.AccountModule),
    data: {breadcrumb: {skip: true}}
  },
  {path: '**', redirectTo: '', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
