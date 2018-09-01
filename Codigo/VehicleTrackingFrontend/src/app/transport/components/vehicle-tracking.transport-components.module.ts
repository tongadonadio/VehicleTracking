import { NgModule } from '@angular/core';
import {CommonModule} from '@angular/common';
import {BrowserModule} from '@angular/platform-browser';
import {FormsModule} from '@angular/forms';
import {VehicleTrackingCommonModule} from '../../common/vehicle-tracking.common.module';
import {VehicleTrackingDisplayTransportsComponent} from './display-transports/vehicle-tracking.transport.display-transports';
import {VehicleTrackingTransportPipesModule} from '../pipes/vehicle-tracking.transport-pipes.module';

@NgModule({
  declarations: [
    VehicleTrackingDisplayTransportsComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    FormsModule,
    VehicleTrackingCommonModule,
    VehicleTrackingTransportPipesModule
  ],
  exports: [
    VehicleTrackingDisplayTransportsComponent
  ],
  providers: [],
  bootstrap: []
})
export class VehicleTrackingTransportComponentsModule { }
