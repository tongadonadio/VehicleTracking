import { NgModule } from '@angular/core';
import {CommonModule} from '@angular/common';
import {BrowserModule} from '@angular/platform-browser';
import {HttpModule} from '@angular/http';
import {SearchBatchPipe} from './search-batch/vehicle-tracking.batch.search-batch';
import {VehiclesForBatchPipe} from './search-batch/vehicle-tracking.batch.vehicles-for-batch';

@NgModule({
  declarations: [
    SearchBatchPipe,
    VehiclesForBatchPipe
  ],
  imports: [
    CommonModule,
    BrowserModule,
    HttpModule
  ],
  exports: [
    SearchBatchPipe,
    VehiclesForBatchPipe
  ],
  providers: [
  ],
  bootstrap: []
})
export class VehicleTrackingBatchPipesModule { }
