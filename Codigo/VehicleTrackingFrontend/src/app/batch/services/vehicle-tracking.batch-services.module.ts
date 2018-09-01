import { NgModule } from '@angular/core';
import {CommonModule} from '@angular/common';
import {BrowserModule} from '@angular/platform-browser';
import {HttpModule} from '@angular/http';
import {VehicleTrackingCommonModule} from '../../common/vehicle-tracking.common.module';
import {VehicleTrackingBatchService} from "./batch/vehicle-tracking.batch.batch-service";

@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    BrowserModule,
    HttpModule,
    VehicleTrackingCommonModule
  ],
  exports: [
  ],
  providers: [
    VehicleTrackingBatchService
  ],
  bootstrap: []
})
export class VehicleTrackingBatchServicesModule { }
