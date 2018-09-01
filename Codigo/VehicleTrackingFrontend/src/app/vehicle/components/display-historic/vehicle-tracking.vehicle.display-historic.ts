import {AfterViewInit, Component, Input, ViewChild} from '@angular/core';
import {VehicleTrackingVehicleService} from '../../services/vehicle/vehicle-tracking.vehicle.vehicle-service';
import {VehicleTrackingLoadingComponent} from '../../../common/components/loading/vehicle-tracking.common.loading';

@Component({
  selector: 'app-display-historic',
  templateUrl: './vehicle-tracking.vehicle.display-historic.html'
})
export class VehicleTrackingDisplayHistoricComponent implements AfterViewInit {
  historic: any;
  vin = '';
  error = false;
  errorServer = false;
  @Input() role: any;
  @ViewChild('loading') loading: VehicleTrackingLoadingComponent;

  constructor(
    private vehicleService: VehicleTrackingVehicleService
  ) { }

  ngAfterViewInit(): void {
  }

  public search(): void {
    this.loading.show();
    this.error = false;
    this.errorServer = false;
    this.vehicleService.getHistoric(this.vin).then(resp => {
      let vehicles = resp.VehicleHistory.sort((a: any, b: any) =>
        new Date(a.Date).getTime() - new Date(b.Date).getTime()
      );
      resp.VehicleHistory = vehicles;
      this.historic = resp;
      this.errorServer = false;
      this.error = false;
      this.loading.hide();
    }).catch(error => {
      console.log(error);
      if (error.status === 0) {
        this.error = false;
        this.errorServer = true;
      } else {
        this.error = true;
        this.errorServer = false;
      }
      this.historic = '';
      this.loading.hide();
    });
  }

}
