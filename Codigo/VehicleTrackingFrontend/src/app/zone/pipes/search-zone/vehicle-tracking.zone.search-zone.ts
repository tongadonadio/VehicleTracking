import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'searchZone',
  pure: false
})
export class SearchZonePipe implements PipeTransform {
  transform(zones: any[], filter: string): any {
    if (!zones || !filter) {
      return zones;
    }
    return zones.filter(z => z.Id.indexOf(filter) !== -1);
  }
}
