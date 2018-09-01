import {AfterViewInit, Component, EventEmitter, Input, Output, ViewChild} from '@angular/core';
import {VehicleTrackingCommonLoginService} from '../../services/login/vehicle-tracking.common.login-service';
import {VehicleTrackingLoadingModalComponent} from '../loading-modal/vehicle-tracking.common.loading-modal';

@Component({
  selector: 'app-vehicle-tracking-login',
  templateUrl: './vehicle-tracking.common.login.html'
})
export class VehicleTrackingLoginComponent implements AfterViewInit{
  constructor(private loginService: VehicleTrackingCommonLoginService) {}
  @ViewChild('loading') loadingModal: VehicleTrackingLoadingModalComponent;

  @Output() confirm = new EventEmitter();
  @Input() title: string;
  password: string;
  username: string;
  error = false;
  errorServer = false;

  ngAfterViewInit(): void {
  }

  private logIn() {
    this.loadingModal.open();
    this.loginService.logIn(this.username, this.password).then(resp => {
      this.error = false;
      this.errorServer = false;
      const user: any = {
        'fullName' : resp.FullName,
        'role' : resp.Role
      };
      this.loginService.setToken(resp.Token);
      this.confirm.emit(user);
      this.loadingModal.close();
    }).catch((error: any) => {
      console.log(error);
      if (error.status === 0) {
        this.errorServer = true;
        this.error = false;
      } else {
        this.errorServer = false;
        this.error = true;
      }
      this.loadingModal.close();
    });
  }

  init(): void {
    this.username = "";
    this.password = "";
  }

  isValidData(): boolean {
    return !!this.username && this.username.length > 0 && !!this.password && this.password.length > 0;
  }

}
