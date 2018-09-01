import {AfterViewInit, ChangeDetectorRef, Component, EventEmitter, Input, Output, ViewChild} from '@angular/core';
import {VehicleTrackingLoadingModalComponent} from '../../../common/components/loading-modal/vehicle-tracking.common.loading-modal';

@Component({
  selector: 'app-create-inspection',
  templateUrl: './vehicle-tracking.inspection.create-inspection.html'
})
export class VehicleTrackingCreateInspectionsComponent implements AfterViewInit {
  @ViewChild('loading') loading: VehicleTrackingLoadingModalComponent;
  location: any;
  @Input() vehicle: any;
  @Input() role: any;
  @Output() confirm = new EventEmitter();
  locations: Array<string> = [];

  constructor(private cdRef: ChangeDetectorRef) {}

  posibleLocations = {
    'Ports': [
      'Puerto 1',
      'Puerto 2',
      'Puerto 3'
    ],
    'Yards': [
      'Patio 1',
      'Patio 2',
      'Patio 3'
    ]
  };

  damages: Array<Damage> = [];

  ngAfterViewInit(): void {
  }

  public init(): void {
    this.locations = [];
    this.damages = [];
    if (this.vehicle.Status === 'En puerto') {
      for (const loc of this.posibleLocations['Ports']) {
        this.locations.push(loc);
      }
    } else if (this.vehicle.Status === 'Esperando por inspeccion patio') {
      for (const loc of this.posibleLocations['Yards']) {
        this.locations.push(loc);
      }
    }
    this.location = this.locations[0];
    this.cdRef.detectChanges();
  }

  addDamage(): void {
    const dmg = new Damage();
    this.damages.push(dmg);
  }

  deleteDamage(dmg: any): void {
    const index = this.damages.indexOf(dmg);
    if (index > -1) {
      this.damages.splice(index, 1);
    }
  }

  deleteImage(dmg: any, img: any): void {
    const index = dmg.UploadedImages.indexOf(img);
    if (index > -1) {
      dmg.UploadedImages.splice(index, 1);
    }
  }

  imageUploaded(data: any, damage: any): void {
    const target = data.target || data.srcElement;
    const file: File = target.files[0];
    const reader: FileReader = new FileReader();

    reader.onloadend = (e) => {
      const image = {
        'base64' : reader.result,
        'name' : file.name
      };
      damage.UploadedImages.push(image);
    }
    reader.readAsDataURL(file);
  }

  public createInspection(): void {
    const inspection: any = {};
    inspection.Location = this.location;
    inspection.IdVehicle = this.vehicle.Vin;
    inspection.Date = new Date();

    let dmgs: any = [];
    for (const d of this.damages) {
      let dmg: any = {
        'Description' : d.Description,
        'Images' : []
      };

      for (const i of d.UploadedImages) {
        const img: any = {
          'Base64EncodedImage' : i.base64
        };
        dmg.Images.push(img);
      }
      dmgs.push(dmg);
    }

    inspection.Damages = dmgs;
    this.confirm.emit(inspection);
  }

  isValidData(): boolean {
    let isValid = true;

    if (this.damages.length > 0) {
      for (const dmg of this.damages) {
        if (!dmg.Description || dmg.Description === '' || dmg.UploadedImages.length === 0) {
          isValid = false;
        }
      }
    }

    return isValid;
  }
}

class Damage {
  Description = '';
  Images: Array<Base64Image> = [];
  UploadedImages: Array<any> = [];
}

class Base64Image {
  Base64EncodedImage = '';
}
