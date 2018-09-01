import { NgModule } from '@angular/core';
import {CommonModule} from '@angular/common';
import {BrowserModule} from '@angular/platform-browser';
import {FormsModule} from '@angular/forms';
import {HttpModule} from '@angular/http';
import {VehicleTrackingZoneComponentsModule} from './components/vehicle-tracking.zone-components.module';
import {VehicleTrackingZoneServicesModule} from './services/vehicle-tracking.zone-services.module';
import {VehicleTrackingZonePipesModule} from './pipes/vehicle-tracking.zone-pipes.module';

@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    BrowserModule,
    FormsModule,
    VehicleTrackingZoneComponentsModule,
    VehicleTrackingZoneServicesModule,
    VehicleTrackingZonePipesModule,
    HttpModule
  ],
  exports: [
    VehicleTrackingZoneComponentsModule,
    VehicleTrackingZoneServicesModule,
    VehicleTrackingZonePipesModule
  ],
  providers: [],
  bootstrap: []
})
export class VehicleTrackingZoneModule { }
