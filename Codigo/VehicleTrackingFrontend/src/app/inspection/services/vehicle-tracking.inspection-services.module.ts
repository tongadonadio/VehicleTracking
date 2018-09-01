import { NgModule } from '@angular/core';
import {CommonModule} from '@angular/common';
import {BrowserModule} from '@angular/platform-browser';
import {HttpModule} from '@angular/http';
import {VehicleTrackingInspectionService} from './inspection/vehicle-tracking.inspection.inspection-service';

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
    VehicleTrackingInspectionService
  ],
  bootstrap: []
})
export class VehicleTrackingInspectionServicesModule { }
