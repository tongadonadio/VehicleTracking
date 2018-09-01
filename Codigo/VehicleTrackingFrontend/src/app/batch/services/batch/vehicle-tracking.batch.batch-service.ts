import {Http, Headers} from '@angular/http';
import {Injectable} from '@angular/core';
import {VehicleTrackingCommonConfigurationService} from '../../../common/services/config/vehicle-tracking.common.configuration-service';
import {VehicleTrackingCommonLoginService} from '../../../common/services/login/vehicle-tracking.common.login-service';

@Injectable()
export class VehicleTrackingBatchService {

  constructor(private http: Http,
              private config: VehicleTrackingCommonConfigurationService,
              private loginService: VehicleTrackingCommonLoginService
  ) { }

  private urlBatches = '/batches';

  public getBatches(): Promise<any> {
    const url = this.config.getVehicleTrackingAPI() + this.urlBatches;
    const header = new Headers({'Content-Type' : 'application/json', 'UserToken' : this.loginService.getToken() });

    return this.http.get(url, {
      headers: header
    }).toPromise()
      .then(resp => resp.json())
      .catch(this.handleError);
  }

  public createBatch(data: any): Promise<any> {
    const url = this.config.getVehicleTrackingAPI() + this.urlBatches;
    const header = new Headers({'Content-Type' : 'application/json', 'UserToken' : this.loginService.getToken() });

    return this.http.post(url, data, {
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
