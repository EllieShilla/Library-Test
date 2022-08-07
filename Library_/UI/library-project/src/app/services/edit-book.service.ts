import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BookDetailsDTO } from '../models/BookDetailsDTO.model';
import { BookForSaveDTO } from '../models/BookForSaveDTO.model';
import { BookIdDTO } from '../models/BookIdDTO.model';

@Injectable({
  providedIn: 'root'
})
export class EditBookService {

  constructor(private http: HttpClient) { }

  apiBaseUrl = environment.apiBaseUrl;

  createBook(data: any): Observable<BookIdDTO> {
    data.Id = 0;
    return this.http.post<BookForSaveDTO>(this.apiBaseUrl + '/api/books/save', data);
  }

  updateBook(data: any) {
    return this.http.post(this.apiBaseUrl + '/api/books/save', data);
  }

  getBookById(id: number): Observable<any> {
    return this.http.get(this.apiBaseUrl + '/api/books/'+ id,{responseType: 'text'});
  }

  DeleteById(id: number, key:string): Observable<string> {
    let params = new HttpParams()
    .set('secret', key);
    return this.http.delete(this.apiBaseUrl + '/api/books/'+ id,{ responseType: 'text', params });
  }
}

