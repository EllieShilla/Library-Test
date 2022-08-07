import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataShareService {
  private dataSource=new BehaviorSubject<string>('Default Data');
  currentData=this.dataSource.asObservable();
  private idSource=new BehaviorSubject<number>(0);
  currentIdData=this.idSource.asObservable();


  updateData(data:string){
    this.dataSource.next(data);
  }

  sendId(id:number){
    this.idSource.next(id);
  }

}
