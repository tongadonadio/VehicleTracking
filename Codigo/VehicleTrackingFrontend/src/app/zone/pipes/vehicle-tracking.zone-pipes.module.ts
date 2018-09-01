import { NgModule } from '@angular/core';
import {CommonModule} from '@angular/common';
import {BrowserModule} from '@angular/platform-browser';
import {HttpModule} from '@angular/http';
import {SearchZonePipe} from './search-zone/vehicle-tracking.zone.search-zone';

@NgModule({
  declarations: [
    SearchZonePipe
  ],
  imports: [
    CommonModule,
    BrowserModule,
    HttpModule
  ],
  exports: [
    SearchZonePipe
  ],
  providers: [
  ],
  bootstrap: []
})
export class VehicleTrackingZonePipesModule { }
