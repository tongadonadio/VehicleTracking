import {Http, Headers} from '@angular/http';
import {Injectable} from '@angular/core';
import {VehicleTrackingCommonConfigurationService} from '../config/vehicle-tracking.common.configuration-service';

@Injectable()
export class VehicleTrackingCommonLoginService {

  constructor(private http: Http, private config: VehicleTrackingCommonConfigurationService) { }

  private token: string;

  private urlLogin = '/users/login';
  private urlLogout = '/users/logout';

  public logIn(userName: string, password: string): Promise<any> {
    const url = this.config.getVehicleTrackingAPI() + this.urlLogin;
    const header = new Headers({'Content-Type' : 'application/json'});
    const loginBody: any = {
      'username' : userName,
      'password' : password
    };

    return this.http.post(url, loginBody, {
      headers: header
    }).toPromise()
      .then(resp => resp.json())
      .catch(this.handleError);
  }

  public logOut(): void {
    const url = this.config.getVehicleTrackingAPI() + this.urlLogout;
    const header = new Headers({'Content-Type' : 'application/json', 'UserToken' : this.token });
    this.token = '';
    this.http.post(url, null, {
      headers: header
    }).toPromise()
      .then(resp => resp.json())
      .catch(this.handleError);
  }

  public getToken(): string {
    return this.token;
  }

  public setToken(token: string) {
    this.token = token;
  }

  private handleError(error: any): Promise<any> {
    console.error('Ha ocurrido un error', error);
    return Promise.reject(error);
  }

}
