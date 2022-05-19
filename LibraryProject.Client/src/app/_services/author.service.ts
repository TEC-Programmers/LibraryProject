import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Author } from '../_models/Author';

@Injectable({
  providedIn: 'root'
})
export class AuthorService {

  private apiUrl = environment.apiUrl + '/Author'

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  constructor(private http:HttpClient){}

  getAllAuthors(): Observable<Author[]>
  {
    return this.http.get<Author[]>(this.apiUrl);
  }

  getAuthor(authorId: number): Observable<Author> {
    return this.http.get<Author>(`${this.apiUrl}/${authorId}`)
  }

  addAuthor(author: Author): Observable<Author>{
    return this.http.post<Author>(this.apiUrl, author, this.httpOptions);
  }

  updateAuthor(authorId: number, author:Author): Observable<Author> {
    return this.http.put<Author>(`${this.apiUrl}/${authorId}`, author, this.httpOptions);
  }


  deleteAuthor(authorid: number): Observable<Author> {
    return this.http.delete<Author>(`${this.apiUrl}/${authorid}`, this.httpOptions);
  }
}
