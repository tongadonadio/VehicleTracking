import { NgModule } from '@angular/core';
import {CommonModule} from '@angular/common';
import {BrowserModule} from '@angular/platform-browser';
import {FormsModule} from '@angular/forms';
import {VehicleTrackingDisplayVehiclesComponent} from './display-vehicles/vehicle-tracking.vehicle.display-vehicles';
import {VehicleTrackingCommonModule} from '../../common/vehicle-tracking.common.module';
import {VehicleTrackingInspectionModule} from '../../inspection/vehicle-tracking.inspection.module';
import {VehicleTrackingVehiclePipesModule} from '../pipes/vehicle-tracking.vehicle-pipes.module';
import {VehicleTrackingDisplayHistoricComponent} from './display-historic/vehicle-tracking.vehicle.display-historic';
import {VehicleTrackingSellVehicleComponent} from './sell-vehicle/vehicle-tracking.vehicle.sell-vehicle';

@NgModule({
  declarations: [
    VehicleTrackingDisplayVehiclesComponent,
    VehicleTrackingDisplayHistoricComponent,
    VehicleTrackingSellVehicleComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    FormsModule,
    VehicleTrackingCommonModule,
    VehicleTrackingInspectionModule,
    VehicleTrackingVehiclePipesModule
  ],
  exports: [
    VehicleTrackingDisplayVehiclesComponent,
    VehicleTrackingDisplayHistoricComponent,
    VehicleTrackingSellVehicleComponent
  ],
  providers: [],
  bootstrap: []
})
export class VehicleTrackingVehicleComponentsModule { }
