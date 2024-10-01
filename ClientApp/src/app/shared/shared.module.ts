import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MaterialModule} from './modules/material/material.module';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {RouterModule} from '@angular/router';
import { BasketSummaryComponent } from './basket-summary/basket-summary.component';
import { OrderTotalsComponent } from './order-totals/order-totals.component';
import { StepperComponent } from './stepper/stepper.component';
import { RatingComponent } from './rating/rating.component';

import {GalleryModule} from 'ng-gallery';
import {FileUploadModule} from 'ng2-file-upload';

@NgModule({
  declarations: [
    BasketSummaryComponent,
    OrderTotalsComponent,
    StepperComponent,
    RatingComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule,
    GalleryModule,
    FileUploadModule,
  ],
  exports: [
    MaterialModule,
    BasketSummaryComponent,
    OrderTotalsComponent,
    StepperComponent,
    RatingComponent,
    GalleryModule,
    FileUploadModule,
  ]
})
export class SharedModule {
}
