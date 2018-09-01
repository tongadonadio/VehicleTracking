import {AfterViewInit, Component, Input, ViewChild} from '@angular/core';
import {NgbModal, NgbModalOptions, NgbModalRef} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-vehicle-tracking-modal',
  templateUrl: './vehicle-tracking.common.modal.html'
})
export class VehicleTrackingModalComponent implements AfterViewInit {
  @Input() title: string;
  @ViewChild('content') content: any;
  private modalRef: NgbModalRef;

  constructor(private modalService: NgbModal) {}

  ngAfterViewInit(): void {
  }

  public open() {
    const options: NgbModalOptions = {
      backdrop : 'static',
      keyboard : false
    };
    this.modalRef = this.modalService.open(this.content, options);
  }

  public close() {
    this.modalRef.close();
  }
}
