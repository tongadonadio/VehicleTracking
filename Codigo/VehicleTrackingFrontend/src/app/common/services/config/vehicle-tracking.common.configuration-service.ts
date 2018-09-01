import {Http} from '@angular/http';
import {Injectable} from '@angular/core';

@Injectable()
export class VehicleTrackingCommonConfigurationService {

  constructor(private http: Http) {
    this.http.get('assets/api-config.json')
      .subscribe(res => this.api = res.json()['url']);
  }

  private api;

  public getVehicleTrackingAPI(): any {
    return this.api;
  }

}
