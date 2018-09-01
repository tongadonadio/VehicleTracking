import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {VehicleTrackingCommonLoginService} from './common/services/login/vehicle-tracking.common.login-service';
import {VehicleTrackingDisplayVehiclesComponent} from './vehicle/components/display-vehicles/vehicle-tracking.vehicle.display-vehicles';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements AfterViewInit {

  title = 'Vehicle Tracking';
  userLogged: any;
  vehicleActive = false;
  inspectionActive = false;
  batchActive = false;
  historicActive = false;
  transportActive = false;
  zoneActive = false;

  constructor(private loginService: VehicleTrackingCommonLoginService) {}

  isUserLogged = false;

  ngAfterViewInit(): void {
  }

  logIn(user: any): void {
    this.isUserLogged = true;
    this.userLogged = user;
    if (this.userLogged.role === 'Transportista') {
      this.batchActive = true;
      this.vehicleActive = false;
      this.inspectionActive = false;
      this.historicActive = false;
      this.transportActive = false;
      this.zoneActive = false;
    } else {
      this.vehicleActive = true;
      this.inspectionActive = false;
      this.batchActive = false;
      this.historicActive = false;
      this.transportActive = false;
      this.zoneActive = false;
    }
  }

  logOut(): void {
    if (this.isUserLogged) {
      this.loginService.logOut();
      this.isUserLogged = false;
    }
  }

  activateVehicle(): void {
    this.vehicleActive = true;
    this.inspectionActive = false;
    this.batchActive = false;
    this.historicActive = false;
    this.transportActive = false;
    this.zoneActive = false;
  }

  activateInspection(): void {
    this.vehicleActive = false;
    this.inspectionActive = true;
    this.batchActive = false;
    this.historicActive = false;
    this.transportActive = false;
    this.zoneActive = false;
  }

  activateBatch(): void {
    this.vehicleActive = false;
    this.inspectionActive = false;
    this.batchActive = true;
    this.historicActive = false;
    this.transportActive = false;
    this.zoneActive = false;
  }

  activateHistoric(): void {
    this.vehicleActive = false;
    this.inspectionActive = false;
    this.batchActive = false;
    this.historicActive = true;
    this.transportActive = false;
    this.zoneActive = false;
  }

  activateTransport(): void {
    this.vehicleActive = false;
    this.inspectionActive = false;
    this.batchActive = false;
    this.historicActive = false;
    this.transportActive = true;
    this.zoneActive = false;
  }

  activateZone(): void {
    this.vehicleActive = false;
    this.inspectionActive = false;
    this.batchActive = false;
    this.historicActive = false;
    this.transportActive = false;
    this.zoneActive = true;
  }

  isUserAllowedToVehicleTab(): boolean {
    return this.userLogged.role === 'Administrador' || this.userLogged.role === 'Operario del Puerto' || this.userLogged.role === 'Operario del Patio' || this.userLogged.role === 'Vendedor';
  }

  isUserAllowedToHistoryTab(): boolean {
    return this.userLogged.role === 'Administrador' || this.userLogged.role === 'Operario del Puerto' || this.userLogged.role === 'Operario del Patio';
  }

  isUserAllowedToInspectionTab(): boolean {
    return this.userLogged.role === 'Administrador' || this.userLogged.role === 'Operario del Puerto' || this.userLogged.role === 'Operario del Patio';
  }

  isUserAllowedToBatchTab(): boolean {
    return this.userLogged.role === 'Administrador' || this.userLogged.role === 'Operario del Puerto' || this.userLogged.role === 'Transportista';
  }

  isUserAllowedToTransportTab(): boolean {
    return this.userLogged.role === 'Administrador' || this.userLogged.role === 'Transportista';
  }

  isUserAllowedToZoneTab(): boolean {
    return this.userLogged.role === 'Administrador' || this.userLogged.role === 'Operario del Patio';
  }

}
