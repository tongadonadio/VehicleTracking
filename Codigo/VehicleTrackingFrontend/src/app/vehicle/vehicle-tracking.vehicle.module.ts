import { NgModule } from '@angular/core';
import {CommonModule} from '@angular/common';
import {BrowserModule} from '@angular/platform-browser';
import {FormsModule} from '@angular/forms';
import {HttpModule} from '@angular/http';
import {VehicleTrackingVehicleComponentsModule} from './components/vehicle-tracking.vehicle-components.module';
import {VehicleTrackingVehicleServicesModule} from './services/vehicle-tracking.vehicle-services.module';
import {VehicleTrackingVehiclePipesModule} from './pipes/vehicle-tracking.vehicle-pipes.module';

@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    BrowserModule,
    FormsModule,
    HttpModule,
    VehicleTrackingVehicleComponentsModule,
    VehicleTrackingVehicleServicesModule,
    VehicleTrackingVehiclePipesModule
  ],
  exports: [
    VehicleTrackingVehicleComponentsModule,
    VehicleTrackingVehicleServicesModule,
    VehicleTrackingVehiclePipesModule
  ],
  providers: [],
  bootstrap: []
})
export class VehicleTrackingVehicleModule { }
