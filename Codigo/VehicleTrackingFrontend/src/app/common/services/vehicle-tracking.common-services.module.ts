import { NgModule } from '@angular/core';
import {CommonModule} from '@angular/common';
import {BrowserModule} from '@angular/platform-browser';
import {VehicleTrackingCommonLoginService} from './login/vehicle-tracking.common.login-service';
import {VehicleTrackingCommonConfigurationService} from './config/vehicle-tracking.common.configuration-service';
import {HttpModule} from '@angular/http';

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
    VehicleTrackingCommonLoginService,
    VehicleTrackingCommonConfigurationService
  ],
  bootstrap: []
})
export class VehicleTrackingCommonServicesModule { }
