import {AfterViewInit, Component, EventEmitter, Input, Output, ViewChild} from '@angular/core';

@Component({
  selector: 'app-sell-vehicle',
  templateUrl: './vehicle-tracking.vehicle.sell-vehicle.html'
})
export class VehicleTrackingSellVehicleComponent implements AfterViewInit {

  error = false;
  name: string;
  price = 1;
  @Input() vehicle;
  @Output() confirm = new EventEmitter();

  constructor() { }

  ngAfterViewInit(): void {
  }

  init(): void {
    this.price = 1;
  }

  sell(): void {
    const vehicle: any = {};
    vehicle.Vin = this.vehicle.Vin;
    vehicle.Price = this.price;
    this.confirm.emit(vehicle);
  }

  isValidData(): boolean {
    let isValid = true;
    isValid = isValid && this.price > 0;

    return isValid;
  }

}
