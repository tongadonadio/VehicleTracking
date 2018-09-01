import { NgModule } from '@angular/core';
import {CommonModule} from '@angular/common';
import {BrowserModule} from '@angular/platform-browser';
import {HttpModule} from '@angular/http';
import {VehicleTrackingZoneService} from './zone/vehicle-tracking.zone.zone-service';

@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    BrowserModule,
    HttpModule
  ],
  exports: [
  ],
  providers: [
    VehicleTrackingZoneService
  ],
  bootstrap: []
})
export class VehicleTrackingZoneServicesModule { }