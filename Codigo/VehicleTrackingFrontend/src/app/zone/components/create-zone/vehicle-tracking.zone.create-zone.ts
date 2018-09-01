import {AfterViewInit, Component, EventEmitter, Input, Output, ViewChild} from '@angular/core';
import {VehicleTrackingLoadingComponent} from '../../../common/components/loading/vehicle-tracking.common.loading';

@Component({
  selector: 'app-create-zone',
  templateUrl: './vehicle-tracking.zone.create-zone.html'
})
export class VehicleTrackingCreateZoneComponent implements AfterViewInit {

  error = false;
  name: string;
  capacity = 1;
  @Output() confirm = new EventEmitter();

  constructor() { }

  ngAfterViewInit(): void {
  }

  init(): void {
    this.name = "";
    this.capacity = 1;
  }

  createZone(): void {
    const zone: any = {};
    zone.Name = this.name;
    zone.MaxCapacity = this.capacity;
    zone.IsSubZone = false;
    zone.Vehicles = [];
    zone.SubZones = [];
    zone.FlowStep = null;

    this.confirm.emit(zone);
  }

  isValidData(): boolean {
    let isValid = true;
    isValid = isValid && !!this.name && this.name !== '';
    isValid = isValid && this.capacity > 0;

    return isValid;
  }

}
