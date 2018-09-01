import { NgModule } from '@angular/core';
import {CommonModule} from '@angular/common';
import {BrowserModule} from '@angular/platform-browser';
import {FormsModule} from '@angular/forms';
import {HttpModule} from '@angular/http';
import {VehicleTrackingBatchComponentsModule} from './components/vehicle-tracking.batch-components.module';
import {VehicleTrackingBatchServicesModule} from './services/vehicle-tracking.batch-services.module';
import {VehicleTrackingCommonModule} from '../common/vehicle-tracking.common.module';
import {VehicleTrackingBatchPipesModule} from './pipes/vehicle-tracking.batch-pipes.module';

@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    BrowserModule,
    FormsModule,
    HttpModule,
    VehicleTrackingBatchComponentsModule,
    VehicleTrackingBatchServicesModule,
    VehicleTrackingCommonModule,
    VehicleTrackingBatchPipesModule
  ],
  exports: [
    VehicleTrackingBatchComponentsModule,
    VehicleTrackingBatchServicesModule,
    VehicleTrackingBatchPipesModule,
    VehicleTrackingBatchPipesModule
  ],
  providers: [],
  bootstrap: []
})
export class VehicleTrackingBatchModule { }
