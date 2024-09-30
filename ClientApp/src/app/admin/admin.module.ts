import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {CoreModule} from '../core/core.module';
import {SharedModule} from '../shared/shared.module';
import { AdminRoutingModule } from './admin-routing.module';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { UserManagementComponent } from './user-management/user-management.component';


@NgModule({
  declarations: [
    AdminPanelComponent,
    UserManagementComponent
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
