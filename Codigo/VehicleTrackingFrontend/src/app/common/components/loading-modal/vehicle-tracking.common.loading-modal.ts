import {AfterViewInit, Component, Input, ViewChild} from '@angular/core';
import {NgbModal, NgbModalOptions, NgbModalRef} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-loading-modal',
  templateUrl: './vehicle-tracking.common.loading-modal.html'
})
export class VehicleTrackingLoadingModalComponent implements AfterViewInit {
  @ViewChild('content') content: any;
  private modalRef: NgbModalRef;

  constructor(private modalService: NgbModal) {}

  ngAfterViewInit(): void {
  }

  public open() {
    const options: NgbModalOptions = {
      backdrop : 'static',
      keyboard : false,
      size : 'sm'
    };
    this.modalRef = this.modalService.open(this.content, options);
  }

  public close() {
    this.modalRef.close();
  }
}
