import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HasRoleDirective } from './directives/has-role.directive';
import { ConfirmDialogComponent } from './modals/confirm-dialog/confirm-dialog.component';
import { RolesModalComponent } from './modals/roles-modal/roles-modal.component';



@NgModule({
  declarations: [
    HasRoleDirective,
    ConfirmDialogComponent,
    RolesModalComponent
  ],
  imports: [
    CommonModule
  ]
})
export class CoreModule { }
