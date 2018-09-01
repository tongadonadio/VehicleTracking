import {Http, Headers} from '@angular/http';
import {Injectable} from '@angular/core';
import {VehicleTrackingCommonConfigurationService} from '../../../common/services/config/vehicle-tracking.common.configuration-service';
import {VehicleTrackingCommonLoginService} from '../../../common/services/login/vehicle-tracking.common.login-service';

@Injectable()
export class VehicleTrackingTransportService {

  constructor(private http: Http,
              private config: VehicleTrackingCommonConfigurationService,
              private loginService: VehicleTrackingCommonLoginService
  ) { }

  private urlTransport = '/transports';

  public getTransports(): Promise<any> {
    const url = this.config.getVehicleTrackingAPI() + this.urlTransport;
    const header = new Headers({'Content-Type' : 'application/json', 'UserToken' : this.loginService.getToken() });

    return this.http.get(url, {
      headers: header
    }).toPromise()
      .then(resp => resp.json())
      .catch(this.handleError);
  }

  public startTransport(data: any): Promise<any> {
    const url = this.config.getVehicleTrackingAPI() + this.urlTransport + '/start';
    const header = new Headers({'Content-Type' : 'application/json', 'UserToken' : this.loginService.getToken() });

    return this.http.post(url, data,{
      headers: header
    }).toPromise()
      .then(resp => resp)
      .catch(this.handleError);
  }

  public finishTransport(id: string): Promise<any> {
    const url = this.config.getVehicleTrackingAPI() + this.urlTransport + '/' + id + '/finish';
    const header = new Headers({'Content-Type' : 'application/json', 'UserToken' : this.loginService.getToken() });

    return this.http.put(url, {},{
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
