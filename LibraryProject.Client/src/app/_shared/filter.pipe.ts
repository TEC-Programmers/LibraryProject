import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filter'
})
export class FilterPipe implements PipeTransform {

  transform(lists: any, filterString: string, propName: string): any[] 
  {
    const result:any =[];
    if(lists || filterString === '' || propName ==='' )
    {
      return lists;
    }
    lists.forEach((a:any)=>{
      if( a[propName].trim().toLowerCase().includes(filterString.toLowerCase)){
        result.push(a);
      }
    });
      return result;

    
  }

}
