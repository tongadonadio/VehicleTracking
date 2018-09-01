import { NgModule } from '@angular/core';
import {CommonModule} from '@angular/common';
import {BrowserModule} from '@angular/platform-browser';
import {FormsModule} from '@angular/forms';
import {HttpModule} from '@angular/http';
import {VehicleTrackingTransportServicesModule} from './services/vehicle-tracking.transport-services.module';
import {VehicleTrackingTransportComponentsModule} from './components/vehicle-tracking.transport-components.module';
import {VehicleTrackingTransportPipesModule} from './pipes/vehicle-tracking.transport-pipes.module';

@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    BrowserModule,
    FormsModule,
    HttpModule,
    VehicleTrackingTransportServicesModule,
    VehicleTrackingTransportComponentsModule,
    VehicleTrackingTransportPipesModule
  ],
  exports: [
    VehicleTrackingTransportServicesModule,
    VehicleTrackingTransportComponentsModule,
    VehicleTrackingTransportPipesModule
  ],
  providers: [],
  bootstrap: []
})
export class VehicleTrackingTransportModule { }
