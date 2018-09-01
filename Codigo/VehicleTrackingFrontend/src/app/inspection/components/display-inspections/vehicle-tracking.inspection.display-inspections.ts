import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {VehicleTrackingInspectionService} from '../../services/inspection/vehicle-tracking.inspection.inspection-service';
import {VehicleTrackingLoadingComponent} from '../../../common/components/loading/vehicle-tracking.common.loading';

@Component({
  selector: 'app-display-inspection',
  templateUrl: './vehicle-tracking.inspection.display-inspection.html'
})
export class VehicleTrackingDisplayInspectionsComponent implements AfterViewInit {
  inspections: Array<Inspection> = [];
  search = '';
  load = true;
  globalError = false;
  @ViewChild('loading') loading: VehicleTrackingLoadingComponent;

  constructor(private inspectionService: VehicleTrackingInspectionService
  ) { }

  ngAfterViewInit(): void {
    this.init();
  }

  public init(): void {
    this.inspections = [];
    this.loading.show();
    this.load = true;
    this.inspectionService.getInspections().then(resp => {
      this.inspections = resp;
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
}

class Inspection {
  Id: string;
  Date: Date;
  Location: string;
  CreatorUserName: string;
  IdVehicle: string;
  Damages: Array<Damage>;
}

class Damage {
  Description: string;
  Images: Array<Base64Image>;
}

class Base64Image {
  Base64EncodedImage: string;
}
