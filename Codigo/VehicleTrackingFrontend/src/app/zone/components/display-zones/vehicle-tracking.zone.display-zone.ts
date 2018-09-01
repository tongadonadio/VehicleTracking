import {AfterViewInit, Component, Input, ViewChild} from '@angular/core';
import {VehicleTrackingZoneService} from '../../services/zone/vehicle-tracking.zone.zone-service';
import {VehicleTrackingVehicleService} from '../../../vehicle/services/vehicle/vehicle-tracking.vehicle.vehicle-service';
import {VehicleTrackingLoadingComponent} from '../../../common/components/loading/vehicle-tracking.common.loading';
import {VehicleTrackingLoadingModalComponent} from '../../../common/components/loading-modal/vehicle-tracking.common.loading-modal';

@Component({
  selector: 'app-display-zones',
  templateUrl: './vehicle-tracking.zone.display-zone.html'
})
export class VehicleTrackingDisplayZonesComponent implements AfterViewInit {
  zones: Array<Zone> = [];
  @Input() user;
  flow: Array<any> = [];
  search = '';
  globalError = false;
  createZoneError = false;
  createSubZoneError = false;
  assignZoneError = false;
  assignVehicleError = false;
  vehicleReadyError = false;
  load = true;
  @ViewChild('loading') loading: VehicleTrackingLoadingComponent;
  @ViewChild('loadingModal') loadingModal: VehicleTrackingLoadingModalComponent;

  constructor(
    private zoneService: VehicleTrackingZoneService,
    private vehicleService: VehicleTrackingVehicleService
  ) { }

  ngAfterViewInit(): void {
    this.init();
  }

  public init(): void {
    this.zones = [];
    this.load = true;
    if(this.isUserAllowedToSeeZoneStuff()) {
      this.loading.show();
      this.zoneService.getZones().then(resp => {
        this.zoneService.getFlow().then(respFlow => {
          let lastStep = respFlow[0].StepNumber;
          let lastStepName = respFlow[0].FlowStep.Name;
          for (const f of respFlow) {
            if (lastStep < f.StepNumber) {
              lastStep = f.StepNumber;
              lastStepName = f.FlowStep.Name;
            }
          }

          let tempZones = resp.filter(z => !z.IsSubZone);

          for (const z of tempZones) {
            for (const sz of z.SubZones) {
              if (sz.FlowStep.Name === lastStepName) {
                for (let v of sz.Vehicles) {
                  v.ReadyToSell = true;
                }
              }
            }
          }

          this.zones = tempZones;

          this.loading.hide();
          this.globalError = false;
          this.load = false;
        }).catch(errorFlow => {
          console.log(errorFlow);
          this.loading.hide();
          this.globalError = true;
          this.load = false;
        });
      }).catch(error => {
        console.log(error);
        this.loading.hide();
        this.globalError = true;
        this.load = false;
      });
    }
  }

  isUserAllowedToSeeZoneStuff(): boolean {
    return this.user.role === 'Administrador' || this.user.role === 'Operario del Patio';
  }

  createNewZone(zone: any): void {
    this.loadingModal.open();
    this.createZoneError = false;
    this.createSubZoneError = false;
    this.assignZoneError = false;
    this.assignVehicleError = false;
    this.vehicleReadyError = false;
    this.zoneService.createZone(zone).then(resp => {
      this.init();
      this.loadingModal.close();
      this.createZoneError = false;
    }).catch(error => {
      console.log(error);
      this.loadingModal.close();
      this.createZoneError = true;
    });
  }

  createNewSubZone(zoneCreated: any): void {
    this.createZoneError = false;
    this.createSubZoneError = false;
    this.assignZoneError = false;
    this.assignVehicleError = false;
    this.vehicleReadyError = false;
    if (zoneCreated) {
      this.init();
      this.createSubZoneError = false;
    } else {
      this.createSubZoneError = true;
    }
  }

  assign(data: any): void {
    this.loadingModal.open();
    this.createZoneError = false;
    this.createSubZoneError = false;
    this.assignZoneError = false;
    this.assignVehicleError = false;
    this.vehicleReadyError = false;
    this.zoneService.assignZone(data.subZoneId, data.zoneId).then(resp => {
      this.init();
      this.loadingModal.close();
      this.assignZoneError = false;
    }).catch(error => {
      console.log(error);
      this.loadingModal.close();
      this.assignZoneError = true;
    });
  }

  assignVehicle(data: any): void {
    this.loadingModal.open();
    this.createZoneError = false;
    this.createSubZoneError = false;
    this.assignZoneError = false;
    this.assignVehicleError = false;
    this.vehicleReadyError = false;
    this.zoneService.assignVehicle(data.vin, data.zoneId).then(resp => {
      this.init();
      this.loadingModal.close();
      this.assignVehicleError = false;
    }).catch(error => {
      console.log(error);
      this.loadingModal.close();
      this.assignVehicleError = true;
    });
  }

  setVehicleReady(vin: string): void {
    this.loadingModal.open();
    this.createZoneError = false;
    this.createSubZoneError = false;
    this.assignZoneError = false;
    this.assignVehicleError = false;
    this.vehicleReadyError = false;
    this.vehicleService.setVehicleReady(vin).then(resp => {
      this.init();
      this.loadingModal.close();
      this.vehicleReadyError = false;
    }).catch(error => {
      console.log(error);
      this.loadingModal.close();
      this.vehicleReadyError = true;
    });
  }
}

class Zone {
  Id: string;
  Name: string;
  IsSubZone: boolean;
  MaxCapacity: number;
  SubZones: Array<Zone>;
  Vehicles: Array<any>;
  FlowStep: FlowStep;
}

class FlowStep {
  Id: string;
  Name: string;
}
