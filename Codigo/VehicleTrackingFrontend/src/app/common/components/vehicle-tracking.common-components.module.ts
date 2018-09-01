import { NgModule } from '@angular/core';
import {CommonModule} from '@angular/common';
import {BrowserModule} from '@angular/platform-browser';
import {VehicleTrackingModalComponent} from './modal/vehicle-tracking.common.modal';
import {VehicleTrackingLoginComponent} from './login/vehicle-tracking.common.login';
import {FormsModule} from '@angular/forms';
import {VehicleTrackingLoadingComponent} from './loading/vehicle-tracking.common.loading';
import {VehicleTrackingLoadingModalComponent} from './loading-modal/vehicle-tracking.common.loading-modal';

@NgModule({
  declarations: [
    VehicleTrackingModalComponent,
    VehicleTrackingLoginComponent,
    VehicleTrackingLoadingComponent,
    VehicleTrackingLoadingModalComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    FormsModule
  ],
  exports: [
    VehicleTrackingModalComponent,
    VehicleTrackingLoginComponent,
    VehicleTrackingLoadingComponent,
    VehicleTrackingLoadingModalComponent
  ],
  providers: [],
  bootstrap: []
})
export class VehicleTrackingCommonComponentsModule { }
