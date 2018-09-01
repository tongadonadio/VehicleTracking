import { NgModule } from '@angular/core';
import {CommonModule} from '@angular/common';
import {BrowserModule} from '@angular/platform-browser';
import {VehicleTrackingCommonComponentsModule} from './components/vehicle-tracking.common-components.module';
import {VehicleTrackingCommonServicesModule} from './services/vehicle-tracking.common-services.module';
import {FormsModule} from '@angular/forms';
import {HttpModule} from '@angular/http';

@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    BrowserModule,
    FormsModule,
    VehicleTrackingCommonComponentsModule,
    VehicleTrackingCommonServicesModule,
    HttpModule
  ],
  exports: [
    VehicleTrackingCommonComponentsModule,
    VehicleTrackingCommonServicesModule
  ],
  providers: [],
  bootstrap: []
})
export class VehicleTrackingCommonModule { }
