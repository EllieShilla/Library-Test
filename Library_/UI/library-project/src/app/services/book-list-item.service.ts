import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BookListItemService {

  constructor(private http: HttpClient) { }

  apiBaseUrl = environment.apiBaseUrl;

  getBookById(id: number): Observable<any> {
    return this.http.get(this.apiBaseUrl + '/api/books/'+ id,{responseType: 'text'});
  }
}
