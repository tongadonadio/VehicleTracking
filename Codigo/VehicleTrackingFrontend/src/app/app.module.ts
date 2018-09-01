import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { VehicleTrackingCommonModule } from './common/vehicle-tracking.common.module';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {FormsModule} from '@angular/forms';
import {HttpModule} from '@angular/http';
import {VehicleTrackingVehicleModule} from './vehicle/vehicle-tracking.vehicle.module';
import {VehicleTrackingBatchModule} from './batch/vehicle-tracking.batch.module';
import {VehicleTrackingInspectionModule} from './inspection/vehicle-tracking.inspection.module';
import {VehicleTrackingTransportModule} from './transport/vehicle-tracking.transport.module';
import {VehicleTrackingZoneModule} from './zone/vehicle-tracking.zone.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    VehicleTrackingCommonModule,
    VehicleTrackingVehicleModule,
    VehicleTrackingBatchModule,
    VehicleTrackingInspectionModule,
    VehicleTrackingTransportModule,
    VehicleTrackingZoneModule,
    NgbModule.forRoot(),
    FormsModule,
    HttpModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
