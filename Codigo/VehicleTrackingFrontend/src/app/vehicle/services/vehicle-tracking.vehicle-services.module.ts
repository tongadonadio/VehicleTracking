import { NgModule } from '@angular/core';
import {CommonModule} from '@angular/common';
import {BrowserModule} from '@angular/platform-browser';
import {HttpModule} from '@angular/http';
import {VehicleTrackingCommonModule} from '../../common/vehicle-tracking.common.module';
import {VehicleTrackingVehicleService} from './vehicle/vehicle-tracking.vehicle.vehicle-service';

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
    VehicleTrackingVehicleService
  ],
  bootstrap: []
})
export class VehicleTrackingVehicleServicesModule { }
