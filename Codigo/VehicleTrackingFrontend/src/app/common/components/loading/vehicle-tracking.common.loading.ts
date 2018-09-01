import {Component} from '@angular/core';

@Component({
  selector: 'app-loading',
  templateUrl: './vehicle-tracking.common.loading.html'
})
export class VehicleTrackingLoadingComponent {

  visible = false;

  public show() {
    setTimeout(() => this.visible = true);
  }

  public hide() {
    setTimeout(() => this.visible = false);
  }
}
