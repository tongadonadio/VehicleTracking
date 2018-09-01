import { NgModule } from '@angular/core';
import {CommonModule} from '@angular/common';
import {BrowserModule} from '@angular/platform-browser';
import {FormsModule} from '@angular/forms';
import {VehicleTrackingDisplayInspectionsComponent} from './display-inspections/vehicle-tracking.inspection.display-inspections';
import {VehicleTrackingCommonModule} from '../../common/vehicle-tracking.common.module';
import {VehicleTrackingCreateInspectionsComponent} from './create-inspection/vehicle-tracking.inspection.create-inspections';
import {VehicleTrackingInspectionPipesModule} from '../pipes/vehicle-tracking.inspection-pipes.module';

@NgModule({
  declarations: [
    VehicleTrackingDisplayInspectionsComponent,
    VehicleTrackingCreateInspectionsComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    FormsModule,
    VehicleTrackingCommonModule,
    VehicleTrackingInspectionPipesModule
  ],
  exports: [
    VehicleTrackingDisplayInspectionsComponent,
    VehicleTrackingCreateInspectionsComponent
  ],
  providers: [],
  bootstrap: []
})
export class VehicleTrackingInspectionComponentsModule { }
