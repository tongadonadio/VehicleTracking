import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'searchTransport',
  pure: false
})
export class SearchTransportPipe implements PipeTransform {
  transform(transports: any[], filter: string): any {
    if (!transports || !filter) {
      return transports;
    }
    return transports.filter(t => t.Id.indexOf(filter) !== -1);
  }
}
