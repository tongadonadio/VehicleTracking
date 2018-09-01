import { NgModule } from '@angular/core';
import {CommonModule} from '@angular/common';
import {BrowserModule} from '@angular/platform-browser';
import {HttpModule} from '@angular/http';
import {VehicleTrackingCommonModule} from '../../common/vehicle-tracking.common.module';
import {VehicleTrackingTransportService} from './transport/vehicle-tracking.transport.transport-service';

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
    VehicleTrackingTransportService
  ],
  bootstrap: []
})
export class VehicleTrackingTransportServicesModule { }
