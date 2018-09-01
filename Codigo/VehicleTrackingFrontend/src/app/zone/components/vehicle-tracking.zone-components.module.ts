import { NgModule } from '@angular/core';
import {CommonModule} from '@angular/common';
import {BrowserModule} from '@angular/platform-browser';
import {FormsModule} from '@angular/forms';
import {VehicleTrackingDisplayZonesComponent} from './display-zones/vehicle-tracking.zone.display-zone';
import {VehicleTrackingCommonModule} from '../../common/vehicle-tracking.common.module';
import {VehicleTrackingZonePipesModule} from '../pipes/vehicle-tracking.zone-pipes.module';
import {VehicleTrackingCreateZoneComponent} from './create-zone/vehicle-tracking.zone.create-zone';
import {VehicleTrackingCreateSubZoneComponent} from './create-subzone/vehicle-tracking.zone.create-subzone';
import {VehicleTrackingMoveZoneComponent} from './move-zone/vehicle-tracking.zone.move-zone';
import {VehicleTrackingAssignVehiclesComponent} from './assign-vehicles/vehicle-tracking.zone.assign-vehicles';

@NgModule({
  declarations: [
    VehicleTrackingDisplayZonesComponent,
    VehicleTrackingCreateZoneComponent,
    VehicleTrackingCreateSubZoneComponent,
    VehicleTrackingMoveZoneComponent,
    VehicleTrackingAssignVehiclesComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    FormsModule,
    VehicleTrackingCommonModule,
    VehicleTrackingZonePipesModule
  ],
  exports: [
    VehicleTrackingDisplayZonesComponent,
    VehicleTrackingCreateZoneComponent,
    VehicleTrackingCreateSubZoneComponent,
    VehicleTrackingMoveZoneComponent,
    VehicleTrackingAssignVehiclesComponent
  ],
  providers: [],
  bootstrap: []
})
export class VehicleTrackingZoneComponentsModule { }
