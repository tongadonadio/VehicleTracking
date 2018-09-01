import { NgModule } from '@angular/core';
import {CommonModule} from '@angular/common';
import {BrowserModule} from '@angular/platform-browser';
import {HttpModule} from '@angular/http';
import {SearchVehiclePipe} from './search-vehicle/vehicle-tracking.vehicle.search-vehicle';

@NgModule({
  declarations: [
    SearchVehiclePipe
  ],
  imports: [
    CommonModule,
    BrowserModule,
    HttpModule
  ],
  exports: [
    SearchVehiclePipe
  ],
  providers: [
  ],
  bootstrap: []
})
export class VehicleTrackingVehiclePipesModule { }
