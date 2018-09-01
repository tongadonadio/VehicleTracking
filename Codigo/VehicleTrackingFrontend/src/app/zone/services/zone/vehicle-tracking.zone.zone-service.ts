import {Http, Headers} from '@angular/http';
import {Injectable} from '@angular/core';
import {VehicleTrackingCommonConfigurationService} from '../../../common/services/config/vehicle-tracking.common.configuration-service';
import {VehicleTrackingCommonLoginService} from '../../../common/services/login/vehicle-tracking.common.login-service';

@Injectable()
export class VehicleTrackingZoneService {

  constructor(private http: Http,
              private config: VehicleTrackingCommonConfigurationService,
              private loginService: VehicleTrackingCommonLoginService
  ) { }

  private urlZones = '/zones';

  public getZones(): Promise<any> {
    const url = this.config.getVehicleTrackingAPI() + this.urlZones;
    const header = new Headers({'Content-Type' : 'application/json', 'UserToken' : this.loginService.getToken() });

    return this.http.get(url, {
      headers: header
    }).toPromise()
      .then(resp => resp.json())
      .catch(this.handleError);
  }

  public getAllSteps(): Promise<any> {
    const url = this.config.getVehicleTrackingAPI() + this.urlZones + '/steps';
    const header = new Headers({'Content-Type' : 'application/json', 'UserToken' : this.loginService.getToken() });

    return this.http.get(url, {
      headers: header
    }).toPromise()
      .then(resp => resp.json())
      .catch(this.handleError);
  }

  public createSubZone(id: string, data: any): Promise<any> {
    const url = this.config.getVehicleTrackingAPI() + this.urlZones + '/' + id;
    const header = new Headers({'Content-Type' : 'application/json', 'UserToken' : this.loginService.getToken() });

    return this.http.post(url, data, {
      headers: header
    }).toPromise()
      .then(resp => resp)
      .catch(this.handleError);
  }

  public createZone(data: any): Promise<any> {
    const url = this.config.getVehicleTrackingAPI() + this.urlZones;
    const header = new Headers({'Content-Type' : 'application/json', 'UserToken' : this.loginService.getToken() });

    return this.http.post(url, data, {
      headers: header
    }).toPromise()
      .then(resp => resp)
      .catch(this.handleError);
  }

  public getFlow(): Promise<any> {
    const url = this.config.getVehicleTrackingAPI() + this.urlZones + '/flow';
    const header = new Headers({'Content-Type' : 'application/json', 'UserToken' : this.loginService.getToken() });

    return this.http.get(url, {
      headers: header
    }).toPromise()
      .then(resp => resp.json())
      .catch(this.handleError);
  }

  public assignZone(subZoneId: any, zoneId: any): Promise<any> {
    const url = this.config.getVehicleTrackingAPI() + this.urlZones + '/subzone/assign?subZoneId=' + subZoneId + '&zoneId=' + zoneId;
    const header = new Headers({'Content-Type' : 'application/json', 'UserToken' : this.loginService.getToken() });

    return this.http.post(url, {}, {
      headers: header
    }).toPromise()
      .then(resp => resp)
      .catch(this.handleError);
  }

  public assignVehicle(vin: any, zoneId: any): Promise<any> {
    const url = this.config.getVehicleTrackingAPI() + this.urlZones + '/vehicle/assign?zoneId=' + zoneId + '&vin=' + vin;
    const header = new Headers({'Content-Type' : 'application/json', 'UserToken' : this.loginService.getToken() });

    return this.http.post(url, {}, {
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
