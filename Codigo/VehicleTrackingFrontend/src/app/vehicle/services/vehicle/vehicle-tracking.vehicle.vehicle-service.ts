import {Http, Headers} from '@angular/http';
import {Injectable} from '@angular/core';
import {VehicleTrackingCommonConfigurationService} from '../../../common/services/config/vehicle-tracking.common.configuration-service';
import {VehicleTrackingCommonLoginService} from '../../../common/services/login/vehicle-tracking.common.login-service';

@Injectable()
export class VehicleTrackingVehicleService {

  constructor(private http: Http,
              private config: VehicleTrackingCommonConfigurationService,
              private loginService: VehicleTrackingCommonLoginService
  ) { }

  private urlVehicles = '/vehicles';

  public getVehicles(): Promise<any> {
    const url = this.config.getVehicleTrackingAPI() + this.urlVehicles;
    const header = new Headers({'Content-Type' : 'application/json', 'UserToken' : this.loginService.getToken() });

    return this.http.get(url, {
      headers: header
    }).toPromise()
      .then(resp => resp.json())
      .catch(this.handleError);
  }

  public getVehiclesByStatus(status: string): Promise<any> {
    const url = this.config.getVehicleTrackingAPI() + this + '/group/' + status;
    const header = new Headers({'Content-Type' : 'application/json', 'UserToken' : this.loginService.getToken() });

    return this.http.get(url, {
      headers: header
    }).toPromise()
      .then(resp => resp.json())
      .catch(this.handleError);
  }

  public getHistoric(vin: string): Promise<any> {
    const url = this.config.getVehicleTrackingAPI() + this.urlVehicles + '/' + vin + '/history';
    const header = new Headers({'Content-Type' : 'application/json', 'UserToken' : this.loginService.getToken() });
    return this.http.get(url, {
      headers: header
    }).toPromise()
      .then(resp => resp.json())
      .catch(this.handleError);
  }

  public setVehicleReady(vin: string): Promise<any> {
    const url = this.config.getVehicleTrackingAPI() + this.urlVehicles + '/' + vin + '/ready';
    const header = new Headers({'Content-Type' : 'application/json', 'UserToken' : this.loginService.getToken() });
    return this.http.post(url, {}, {
      headers: header
    }).toPromise()
      .then(resp => resp)
      .catch(this.handleError);
  }

  public sellVehicle(data: any): Promise<any> {
    const url = this.config.getVehicleTrackingAPI() + this.urlVehicles + '/sell' ;
    const header = new Headers({'Content-Type' : 'application/json', 'UserToken' : this.loginService.getToken() });
    return this.http.put(url, data, {
      headers: header
    }).toPromise()
      .then(resp => resp)
      .catch(this.handleError);
  }

  private handleError(error: any): Promise<any> {
    console.error('Ha ocurrido un error', error);
    return Promise.reject(error);
  }

}
