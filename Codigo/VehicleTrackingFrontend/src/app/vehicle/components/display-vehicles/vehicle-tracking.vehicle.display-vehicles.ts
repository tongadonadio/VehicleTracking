import {AfterViewInit, Component, Input, ViewChild} from '@angular/core';
import {VehicleTrackingVehicleService} from '../../services/vehicle/vehicle-tracking.vehicle.vehicle-service';
import {VehicleTrackingLoadingComponent} from '../../../common/components/loading/vehicle-tracking.common.loading';
import {VehicleTrackingInspectionService} from '../../../inspection/services/inspection/vehicle-tracking.inspection.inspection-service';
import {VehicleTrackingLoadingModalComponent} from '../../../common/components/loading-modal/vehicle-tracking.common.loading-modal';

@Component({
  selector: 'app-display-vehicles',
  templateUrl: './vehicle-tracking.vehicle.display-vehicles.html'
})
export class VehicleTrackingDisplayVehiclesComponent implements AfterViewInit {
  vehicles: Array<any> = [];
  search = '';
  load = true;
  globalError = false;
  inspectionError = false;
  sellError = false;
  @Input() role: any;
  @ViewChild('loading') loading: VehicleTrackingLoadingComponent;
  @ViewChild('loadingModal') loadingModal: VehicleTrackingLoadingModalComponent;

  constructor(
    private vehicleService: VehicleTrackingVehicleService,
    private inspectionService: VehicleTrackingInspectionService
  ) { }

  ngAfterViewInit(): void {
    this.init();
  }

  public init(): void {
    this.vehicles = [];
    this.load = true;
    this.loading.show();
    this.vehicleService.getVehicles().then(resp => {
      this.vehicles = resp;
      this.loading.hide();
      this.globalError = false;
      this.load = false;
    }).catch(error => {
      console.log(error);
      this.loading.hide();
      this.globalError = true;
      this.load = false;
    });
  }

  public createInsp(data: any): void {
    this.loadingModal.open();
    this.sellError = false;
    this.inspectionError = false;
    this.inspectionService.createInspection(data).then(resp => {
      this.init();
      this.loadingModal.close();
      this.inspectionError = false;
    }).catch(error => {
      console.log(error);
      this.loadingModal.close();
      this.inspectionError = true;
    });
  }

  isCreationAllowed(vehicle: any): boolean {
    return !!vehicle && (vehicle.Status === 'En puerto' || vehicle.Status === 'Esperando por inspeccion patio');
  }

  isUserAllowedToSell(vehicle: any): boolean {
    return !!vehicle && vehicle.Status === 'Listo para la venta';
  }

  isUserNotAllowedToCreateInspection() {
    return this.role === 'Transportista' || this.role === 'Vendedor';
  }

  isUserNotAllowedToSellVehicle() {
    return this.role === 'Transportista' || this.role === 'Operario del Puerto' || this.role === 'Operario del Patio';
  }

  sell(data: any) {
    this.loadingModal.open();
    this.sellError = false;
    this.inspectionError = false;
    this.vehicleService.sellVehicle(data).then(resp => {
      this.init();
      this.loadingModal.close();
      this.sellError = false;
    }).catch(error => {
      console.log(error);
      this.loadingModal.close();
      this.sellError = true;
    });
  }

}
