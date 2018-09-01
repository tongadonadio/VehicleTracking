import {AfterViewInit, Component, Input, ViewChild} from '@angular/core';
import {VehicleTrackingLoadingComponent} from '../../../common/components/loading/vehicle-tracking.common.loading';
import {VehicleTrackingLoadingModalComponent} from '../../../common/components/loading-modal/vehicle-tracking.common.loading-modal';
import {VehicleTrackingTransportService} from '../../../transport/services/transport/vehicle-tracking.transport.transport-service';

@Component({
  selector: 'app-display-transports',
  templateUrl: './vehicle-tracking.transport.display-transports.html'
})
export class VehicleTrackingDisplayTransportsComponent implements AfterViewInit {
  transports: Array<any> = [];
  @Input() user;
  search = '';
  load = true;
  globalError = false;
  stopTransportError = false;
  @ViewChild('loading') loading: VehicleTrackingLoadingComponent;
  @ViewChild('loadingModal') loadingModal: VehicleTrackingLoadingModalComponent;

  constructor(
    private transportService: VehicleTrackingTransportService
  ) { }

  ngAfterViewInit(): void {
    this.init();
  }

  public init(): void {
    this.load = true;
    this.transports = [];
    if(this.isUserAllowedToSeeTransportationStuff()) {
      this.loading.show();
      this.transportService.getTransports().then(resp => {
        const transp = resp;
        for (const t of transp) {
          if (!!t.StartDate && t.StartDate !== '' && !!t.EndDate && t.EndDate !== '' && t.EndDate > t.StartDate) {
            t.TransportStatus = 'Transportado';
          } else if (!!t.StartDate && t.StartDate !== '') {
            t.TransportStatus = 'En transporte';
          }
        }
        this.transports = transp;
        this.loading.hide();
        this.globalError = false;
        this.load = false;
      }).catch(error => {
        this.globalError = true;
        this.loading.hide();
        this.load = false;
      });
    }
  }

  isUserAllowedToSeeTransportationStuff(): boolean {
    return this.user.role === 'Administrador' || this.user.role === 'Transportista';
  }

  isFinishTransportReady(): boolean {
    let isReady = false;
    for (const t of this.transports) {
      if (t.Selected) {
        isReady = true;
      }
    }
    return isReady;
  }

  public transportSelected(tr: any) {
    for (const t of this.transports) {
      if (t.Id !== tr.Id) {
        t.Selected = false;
      }
    }
  }

  finishTransport(): void {
    let toTransport = null;
    for (const t of this.transports) {
      if (t.Selected) {
        toTransport = t.Id;
      }
    }
    this.stopTransportError = false;
    this.loadingModal.open();
    this.transportService.finishTransport(toTransport).then(resp => {
      this.init();
      this.loadingModal.close();
      this.stopTransportError = false;
    }).catch(error => {
      console.log(error);
      this.loadingModal.close();
      this.stopTransportError = true;
    });
  }

}
