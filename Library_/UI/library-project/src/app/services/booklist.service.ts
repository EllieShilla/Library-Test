import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BookDTO } from '../models/BookDTO.model';

@Injectable({
  providedIn: 'root'
})
export class BooklistService {

  constructor(private http: HttpClient) { }

  apiBaseUrl = environment.apiBaseUrl;

  getAllBooks(): Observable<any> {
    return this.http.get(this.apiBaseUrl + '/api/books', { responseType: 'text' });
  }

  RecommendedBook(genre: string): Observable<any> {
    let params = new HttpParams()
      .set('genre', genre);
    return this.http.get(this.apiBaseUrl + '/api/recommended?', { responseType: 'text', params });
  }


}
