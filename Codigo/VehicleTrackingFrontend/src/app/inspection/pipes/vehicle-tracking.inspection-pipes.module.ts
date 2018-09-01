import { NgModule } from '@angular/core';
import {CommonModule} from '@angular/common';
import {BrowserModule} from '@angular/platform-browser';
import {HttpModule} from '@angular/http';
import {SearchInspectionPipe} from './search-inspection/vehicle-tracking.inspection.search-inspection';

@NgModule({
  declarations: [
    SearchInspectionPipe
  ],
  imports: [
    CommonModule,
    BrowserModule,
    HttpModule
  ],
  exports: [
    SearchInspectionPipe
  ],
  providers: [
  ],
  bootstrap: []
})
export class VehicleTrackingInspectionPipesModule { }
