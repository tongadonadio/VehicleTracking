import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'searchBatch',
  pure: false
})
export class SearchBatchPipe implements PipeTransform {
  transform(batches: any[], filter: string): any {
    if (!batches || !filter) {
      return batches;
    }
    return batches.filter(i => i.Id.indexOf(filter) !== -1);
  }
}
