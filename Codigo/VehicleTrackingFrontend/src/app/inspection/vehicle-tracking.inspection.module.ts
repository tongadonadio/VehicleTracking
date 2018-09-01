import { NgModule } from '@angular/core';
import {CommonModule} from '@angular/common';
import {BrowserModule} from '@angular/platform-browser';
import {FormsModule} from '@angular/forms';
import {HttpModule} from '@angular/http';
import {VehicleTrackingInspectionComponentsModule} from './components/vehicle-tracking.inspection-components.module';
import {VehicleTrackingInspectionServicesModule} from './services/vehicle-tracking.inspection-services.module';
import {VehicleTrackingInspectionPipesModule} from './pipes/vehicle-tracking.inspection-pipes.module';

@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    BrowserModule,
    FormsModule,
    VehicleTrackingInspectionComponentsModule,
    VehicleTrackingInspectionServicesModule,
    VehicleTrackingInspectionPipesModule,
    HttpModule
  ],
  exports: [
    VehicleTrackingInspectionComponentsModule,
    VehicleTrackingInspectionServicesModule,
    VehicleTrackingInspectionPipesModule
  ],
  providers: [],
  bootstrap: []
})
export class VehicleTrackingInspectionModule { }
