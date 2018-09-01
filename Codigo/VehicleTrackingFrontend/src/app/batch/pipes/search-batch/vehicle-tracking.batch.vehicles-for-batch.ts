import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'vehiclesForBatch',
  pure: false
})
export class VehiclesForBatchPipe implements PipeTransform {
  transform(vehicles: any[]): any {
    if (!vehicles || vehicles.length <= 0) {
      return vehicles;
    }
    return vehicles.filter(v => !!v.Status && v.Status.indexOf('Listo para partir en puerto') !== -1);
  }
}
