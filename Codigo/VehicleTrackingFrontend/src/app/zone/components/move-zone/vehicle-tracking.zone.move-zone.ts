import {AfterViewInit, Component, EventEmitter, Input, Output, ViewChild} from '@angular/core';
import {VehicleTrackingLoadingComponent} from '../../../common/components/loading/vehicle-tracking.common.loading';
import {VehicleTrackingZoneService} from '../../services/zone/vehicle-tracking.zone.zone-service';
import {VehicleTrackingLoadingModalComponent} from '../../../common/components/loading-modal/vehicle-tracking.common.loading-modal';

@Component({
  selector: 'app-move-zone',
  templateUrl: './vehicle-tracking.zone.move-zone.html'
})
export class VehicleTrackingMoveZoneComponent implements AfterViewInit {
  @ViewChild('loading') loading: VehicleTrackingLoadingComponent;
  @ViewChild('loadingModal') loadingModal: VehicleTrackingLoadingModalComponent;

  error = false;
  load = true;
  zones: Array<any> = [];
  @Input() subZone: any;
  @Output() confirm = new EventEmitter();

  constructor(
    private zoneService: VehicleTrackingZoneService
  ) { }

  ngAfterViewInit(): void {
    this.init();
  }

  public init(): void {
    this.zones = [];
    this.loading.show();
    this.error = false;
    this.load = true;
    this.zoneService.getZones().then(resp => {
      let mainZones: Array<any> = [];
      mainZones = resp.filter(z => !z.IsSubZone);
      this.filterByCapacity(mainZones);
      this.loading.hide();
      this.load = false;
    }).catch(error => {
      console.log(error);
      this.loading.hide();
      this.error = true;
    });
  }

  public zoneSelected(zo: any) {
    for (const z of this.zones) {
      if (z.Id !== zo.Id) {
        z.Selected = false;
      }
    }
  }

  private filterByCapacity(mainZones: Array<any>): void {
    for (const zo of mainZones) {
      let maxCap = zo.MaxCapacity;
      for (const zz of zo.SubZones) {
        maxCap = maxCap - zz.MaxCapacity;
      }
      if (this.subZone.MaxCapacity <= maxCap) {
        this.zones.push(zo);
      }
    }
  }

  isValidData(): boolean {
    let isValid = false;

    for (const z of this.zones) {
      isValid = isValid || z.Selected;
    }

    return isValid;
  }

  public assign(): void {
    let zo;
    for (const z of this.zones) {
      if (z.Selected) {
        zo = z;
      }
    }
    const asg = {
      'subZoneId': this.subZone.Id,
      'zoneId': zo.Id
    };

    this.confirm.emit(asg);
  }

}

