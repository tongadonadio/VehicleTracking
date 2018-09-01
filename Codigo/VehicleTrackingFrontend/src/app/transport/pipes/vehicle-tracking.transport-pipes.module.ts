import { NgModule } from '@angular/core';
import {CommonModule} from '@angular/common';
import {BrowserModule} from '@angular/platform-browser';
import {HttpModule} from '@angular/http';
import {SearchTransportPipe} from './search-transport/vehicle-tracking.transport.search-transport';

@NgModule({
  declarations: [
    SearchTransportPipe
  ],
  imports: [
    CommonModule,
    BrowserModule,
    HttpModule
  ],
  exports: [
    SearchTransportPipe
  ],
  providers: [
  ],
  bootstrap: []
})
export class VehicleTrackingTransportPipesModule { }
