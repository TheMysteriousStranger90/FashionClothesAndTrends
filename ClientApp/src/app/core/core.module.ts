import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HasRoleDirective } from './directives/has-role.directive';
import { ConfirmDialogComponent } from './modals/confirm-dialog/confirm-dialog.component';
import { RolesModalComponent } from './modals/roles-modal/roles-modal.component';
import { SharedModule } from '../shared/shared.module';
import { RouterModule } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';
import { MatDialogModule } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
import { NgxSpinnerModule } from 'ngx-spinner';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { BreadcrumbModule } from 'xng-breadcrumb';

@NgModule({
  declarations: [
    HasRoleDirective,
    ConfirmDialogComponent,
    RolesModalComponent,
    NavBarComponent,
  ],
  imports: [
    BreadcrumbModule,
    MatDialogModule,
    FormsModule,
    NgxSpinnerModule,

    SharedModule,
    CommonModule,
    RouterModule,
    ToastrModule.forRoot(
      { positionClass: 'toast-bottom-right',
        preventDuplicates: true})
  ],
  exports: [
    NavBarComponent,
    HasRoleDirective,
    RolesModalComponent,
    NgxSpinnerModule,
    ConfirmDialogComponent
  ]
})
export class CoreModule { }
