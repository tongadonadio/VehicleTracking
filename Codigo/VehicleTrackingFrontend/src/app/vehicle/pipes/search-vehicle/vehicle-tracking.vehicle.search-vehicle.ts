import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'searchVehicle',
  pure: false
})
export class SearchVehiclePipe implements PipeTransform {
  transform(vehicles: any[], filter: string): any {
    if (!vehicles || !filter) {
      return vehicles;
    }
    return vehicles.filter(v => v.Vin.indexOf(filter) !== -1);
  }
}
