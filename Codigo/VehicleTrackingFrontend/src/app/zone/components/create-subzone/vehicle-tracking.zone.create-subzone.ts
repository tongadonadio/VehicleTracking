import {AfterViewInit, Component, EventEmitter, Input, Output, ViewChild} from '@angular/core';
import {VehicleTrackingLoadingComponent} from '../../../common/components/loading/vehicle-tracking.common.loading';
import {VehicleTrackingZoneService} from '../../services/zone/vehicle-tracking.zone.zone-service';
import {VehicleTrackingLoadingModalComponent} from '../../../common/components/loading-modal/vehicle-tracking.common.loading-modal';

@Component({
  selector: 'app-create-subzone',
  templateUrl: './vehicle-tracking.zone-create-subzone.html'
})
export class VehicleTrackingCreateSubZoneComponent implements AfterViewInit {
  @ViewChild('loading') loading: VehicleTrackingLoadingComponent;
  @ViewChild('loadingModal') loadingModal: VehicleTrackingLoadingModalComponent;

  error = false;
  load = true;
  name: string;
  capacity = 1;
  maxCap = 1;
  flowStep: string;
  flowSteps: Array<any> = [];
  @Input() mainZone: any;
  @Output() confirm = new EventEmitter();

  constructor(
    private zoneService: VehicleTrackingZoneService
  ) { }

  ngAfterViewInit(): void {
    this.init();
  }

  createModel(): any {
    const zone: any = {};
    zone.Name = this.name;
    zone.IsSubZone = true;
    zone.MaxCapacity = this.capacity;
    zone.FlowStep = {
      'Name' : this.flowStep
    };
    zone.SubZones = [];
    zone.Vehicles = [];

    return zone;
  }

  createSubZone(): void {
    let zone = this.createModel();
    this.loadingModal.open();
    this.zoneService.createSubZone(this.mainZone.Id, zone).then(resp => {
      this.confirm.emit(true);
      this.loadingModal.close();
    }).catch(error => {
      console.log(error);
      this.loadingModal.close();
      this.confirm.emit(false);
    });
  }

  public init(): void {
    this.flowSteps = [];
    this.name = "";
    this.capacity = 1;
    this.loading.show();
    this.load = true;
    this.error = false;
    this.zoneService.getAllSteps().then(resp => {
      this.flowSteps = resp;
      this.flowStep = this.flowSteps[0].Name;
      this.load = false;
      this.error = false;
      this.loading.hide();
    }).catch(error => {
      console.log(error);
      this.loading.hide();
      this.error = true;
      this.load = false;
    });
    if(!!this.mainZone) {
      this.calculateCap();
    }
  }

  isValidData(): boolean {
    let isValid = true;

    isValid = isValid && this.maxCap > 0;
    isValid = isValid && this.capacity <= this.maxCap;
    isValid = isValid && !!this.name && this.name !== '';
    isValid = isValid && this.capacity > 0;

    return isValid;
  }

  private calculateCap(): void {
    this.maxCap = this.mainZone.MaxCapacity;
    for (let z of this.mainZone.SubZones) {
      this.maxCap = this.maxCap - z.MaxCapacity;
    }
  }
}
