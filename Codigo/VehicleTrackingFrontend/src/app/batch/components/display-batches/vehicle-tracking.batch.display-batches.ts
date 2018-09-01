import {AfterViewInit, Component, Input, ViewChild} from '@angular/core';
import {VehicleTrackingBatchService} from '../../services/batch/vehicle-tracking.batch.batch-service';
import {VehicleTrackingLoadingComponent} from '../../../common/components/loading/vehicle-tracking.common.loading';
import {VehicleTrackingLoadingModalComponent} from '../../../common/components/loading-modal/vehicle-tracking.common.loading-modal';
import {VehicleTrackingTransportService} from '../../../transport/services/transport/vehicle-tracking.transport.transport-service';

@Component({
  selector: 'app-display-batches',
  templateUrl: './vehicle-tracking.batch.display-batches.html'
})
export class VehicleTrackingDisplayBatchesComponent implements AfterViewInit {
  batches: Array<any> = [];
  @Input() user;
  search = '';
  globalError = false;
  createBatchError = false;
  startTransportError = false;
  load = true;
  @ViewChild('loading') loading: VehicleTrackingLoadingComponent;
  @ViewChild('loadingModal') loadingModal: VehicleTrackingLoadingModalComponent;

  constructor(
    private batchService: VehicleTrackingBatchService,
    private transportService: VehicleTrackingTransportService
  ) { }

  ngAfterViewInit(): void {
    this.init();
  }

  public init(): void {
    this.batches = [];
    this.load = true;
    if(this.isUserAllowedToSeeBatchStuff()) {
      this.loading.show();
      this.batchService.getBatches().then(resp => {
        let batches = resp;
        if (this.isUserAllowedToSeeTransportationStuff()) {
          this.transportService.getTransports().then(respT => {
            const transp = respT;
            for (const t of transp) {
              for (const batchTrans of t.Batches) {
                for (const b of batches) {
                  if (batchTrans.Id === b.Id) {
                    if (!!t.StartDate && t.StartDate !== '' && !!t.EndDate && t.EndDate !== '' && t.EndDate > t.StartDate) {
                      b.StartDate = t.StartDate;
                      b.EndDate = t.EndDate;
                      b.TransportStatus = 'Transportado';
                    } else if (!!t.StartDate && t.StartDate !== '') {
                      b.StartDate = t.StartDate;
                      b.TransportStatus = 'En transporte';
                    }
                  }
                }
              }
            }
          }).catch(errorT => {
            console.log(errorT);
            this.globalError = true;
            this.loading.hide();
            this.load = false;
          });
        }
        this.batches = batches;
        this.globalError = false;
        this.loading.hide();
        this.load = false;
      }).catch(error => {
        console.log(error);
        this.globalError = true;
        this.loading.hide();
        this.load = false;
      });
    }
  }

  public isUserAllowedToSeeTransportationStuff(): boolean {
    return this.user.role === 'Administrador' || this.user.role === 'Transportista';
  }

  public isUserAllowedToCreateBatches(): boolean {
    return this.user.role === 'Administrador' || this.user.role === 'Operario del Puerto';
  }

  isUserAllowedToSeeBatchStuff(): boolean {
    return this.user.role === 'Administrador' || this.user.role === 'Operario del Puerto' || this.user.role === 'Transportista';
  }

  beginTransport(): void {
    let batchesToTransport = [];
    for (const b of this.batches) {
      if (b.Selected) {
        batchesToTransport.push(b.Id);
      }
    }
    this.loadingModal.open();
    this.createBatchError = false;
    this.startTransportError = false;
    this.transportService.startTransport(batchesToTransport).then(resp => {
      this.init();
      this.loadingModal.close();
      this.startTransportError = false;
    }).catch(error => {
      console.log(error);
      this.loadingModal.close();
      this.startTransportError = true;
    });
  }

  isBeginTransportReady(): boolean {
    let isReady = false;
    for (const b of this.batches) {
      if (b.Selected) {
        isReady = true;
      }
    }
    return isReady;
  }

  createNewBatch(batch: any): void {
    this.loadingModal.open();
    this.createBatchError = false;
    this.startTransportError = false;
    this.batchService.createBatch(batch).then(resp => {
      this.init();
      this.loadingModal.close();
      this.createBatchError = false;
    }).catch(error => {
      console.log(error);
      this.loadingModal.close();
      this.createBatchError = true;
    });
  }

}
