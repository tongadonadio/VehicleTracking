import {AfterViewInit, Component, EventEmitter, Input, Output, ViewChild} from '@angular/core';
import {VehicleTrackingVehicleService} from '../../../vehicle/services/vehicle/vehicle-tracking.vehicle.vehicle-service';
import {VehicleTrackingLoadingComponent} from '../../../common/components/loading/vehicle-tracking.common.loading';

@Component({
  selector: 'app-create-batch',
  templateUrl: './vehicle-tracking.batch.create-batch.html'
})
export class VehicleTrackingCreateBatchComponent implements AfterViewInit {
  @ViewChild('loading') loading: VehicleTrackingLoadingComponent;

  error = false;
  load = true;
  description: string;
  name: string;
  vehicles: Array<any> = [];
  @Output() confirm = new EventEmitter();

  constructor(
    private vehicleService: VehicleTrackingVehicleService
  ) { }

  ngAfterViewInit(): void {
  }

  public loadVehicles() {
    this.loading.show();
    this.description = "";
    this.name = "";
    this.load = true;
    this.vehicleService.getVehicles().then(resp => {
      this.vehicles = resp.filter(v => v.Status === 'Inspeccionado en puerto');
      this.loading.hide();
      this.load = false;
      this.error = false;
    }).catch(error => {
      console.log(error);
      this.loading.hide();
      this.load = false;
      this.error = true;
    });
  }

  createBatch(): void {
    const batch: any = {};
    batch.Description = this.description;
    batch.Name = this.name;

    let batchVehicles: any = [];
    for (const v of this.vehicles) {
      if (v.Selected) {
        batchVehicles.push(v.Vin);
      }
    }

    batch.Vehicles = batchVehicles;

    this.confirm.emit(batch);
  }

  isValidData(): boolean {
    let isValid = false;

    if (this.vehicles.length > 0) {
      if (!!this.description && !(this.description === '') && (!!this.name) && !(this.name === '')) {
        for (const v of this.vehicles) {
          if(v.Selected) {
            isValid = true;
          }
        }
      }
    }

    return isValid;
  }
}
