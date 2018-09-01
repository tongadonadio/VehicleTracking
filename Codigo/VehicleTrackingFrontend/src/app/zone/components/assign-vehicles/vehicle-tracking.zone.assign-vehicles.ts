import {AfterViewInit, Component, EventEmitter, Input, Output, ViewChild} from '@angular/core';
import {VehicleTrackingLoadingComponent} from '../../../common/components/loading/vehicle-tracking.common.loading';
import {VehicleTrackingZoneService} from '../../services/zone/vehicle-tracking.zone.zone-service';
import {VehicleTrackingLoadingModalComponent} from '../../../common/components/loading-modal/vehicle-tracking.common.loading-modal';
import {VehicleTrackingVehicleService} from '../../../vehicle/services/vehicle/vehicle-tracking.vehicle.vehicle-service';

@Component({
  selector: 'app-assign-vehicles',
  templateUrl: './vehicle-tracking.zone.assign-vehicles.html'
})
export class VehicleTrackingAssignVehiclesComponent implements AfterViewInit {
  @ViewChild('loading') loading: VehicleTrackingLoadingComponent;
  @ViewChild('loadingModal') loadingModal: VehicleTrackingLoadingModalComponent;
  error = false;
  isLoading = true;
  vehicles: Array<any> = [];
  @Input() zone: any;
  @Output() confirm = new EventEmitter();

  constructor(private zoneService: VehicleTrackingZoneService,
              private vehicleService: VehicleTrackingVehicleService) {
  }

  ngAfterViewInit(): void {
    this.loading.show();
  }

  public init(): void {
    this.loading.show();
    this.error = false;
    this.isLoading = true;
    this.vehicleService.getVehicles().then(resp => {
      let mainVehicles: Array<any> = [];
      mainVehicles = resp.filter(v => v.Status === 'Listo para ser ubicado en zona');
      this.zoneService.getZones().then(zones => {
        this.zoneService.getFlow().then(flow => {
          this.filterByFlow(mainVehicles, zones, flow);
          this.error = false;
        }).catch(errorFlow => {
            console.log(errorFlow);
            this.loading.hide();
            this.isLoading = false;
            this.error = true;
        });
      }).catch(errorZone => {
          console.log(errorZone);
          this.loading.hide();
          this.isLoading = false;
          this.error = true;
      });
    }).catch(error => {
      console.log(error);
      this.loading.hide();
      this.isLoading = false;
      this.error = true;
    });
  }

  public vehicleSelected(ve: any) {
    for (const v of this.vehicles) {
      if (v.Vin !== ve.Vin) {
        v.Selected = false;
      }
    }
  }

  private filterByFlow(mainVehicles: Array<any>, allZones: Array<any>, flow: any): void {
    let currentStep = 0;
    let currentStepName = '';
    let prevStepName = '';
    for (const f of flow) {
      if (this.zone.FlowStep.Name === f.FlowStep.Name) {
        currentStep = f.StepNumber;
        currentStepName = f.FlowStep.Name;
      }
    }
    for (const f of flow) {
      if ((currentStep - 1) === f.StepNumber) {
        prevStepName = f.FlowStep.Name;
      }
    }

    if (currentStep !== 1) {
      mainVehicles = [];
    }

    for (const z of allZones) {
      for (const sz of z.SubZones) {
        for (const v of sz.Vehicles) {
          if (sz.FlowStep.Name === prevStepName && this.zone.FlowStep.Name === currentStepName) {
            mainVehicles.push(v);
          }
        }
      }
    }

    this.vehicles = mainVehicles;
    this.isLoading = false;
    this.loading.hide();
  }

  isValidData(): boolean {
    let isValid = false;

    for (const v of this.vehicles) {
      isValid = isValid || v.Selected;
    }

    return isValid;
  }

  public assign(): void {
    let ve;
    for (const v of this.vehicles) {
      if (v.Selected) {
        ve = v;
      }
    }
    const asg = {
      'vin': ve.Vin,
      'zoneId': this.zone.Id
    };

    this.confirm.emit(asg);
  }
}
