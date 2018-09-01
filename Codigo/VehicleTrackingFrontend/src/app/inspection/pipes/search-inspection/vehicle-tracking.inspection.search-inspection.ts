import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'searchInspection',
  pure: false
})
export class SearchInspectionPipe implements PipeTransform {
  transform(inspections: any[], filter: string): any {
    if (!inspections || !filter) {
      return inspections;
    }
    return inspections.filter(i => i.Id.indexOf(filter) !== -1);
  }
}
