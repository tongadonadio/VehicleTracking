import { NgModule } from '@angular/core';
import {CommonModule} from '@angular/common';
import {BrowserModule} from '@angular/platform-browser';
import {FormsModule} from '@angular/forms';
import {VehicleTrackingDisplayBatchesComponent} from './display-batches/vehicle-tracking.batch.display-batches';
import {VehicleTrackingCommonModule} from '../../common/vehicle-tracking.common.module';
import {VehicleTrackingBatchPipesModule} from '../pipes/vehicle-tracking.batch-pipes.module';
import {VehicleTrackingCreateBatchComponent} from './create-batch/vehicle-tracking.batch.create-batch';

@NgModule({
  declarations: [
    VehicleTrackingDisplayBatchesComponent,
    VehicleTrackingCreateBatchComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    FormsModule,
    VehicleTrackingCommonModule,
    VehicleTrackingBatchPipesModule
  ],
  exports: [
    VehicleTrackingDisplayBatchesComponent,
    VehicleTrackingCreateBatchComponent
  ],
  providers: [],
  bootstrap: []
})
export class VehicleTrackingBatchComponentsModule { }
