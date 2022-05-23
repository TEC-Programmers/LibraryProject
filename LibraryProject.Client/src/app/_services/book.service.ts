import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Book } from '../_models/Book';
import { Category } from '../_models/Category';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  private apiUrl = environment.apiUrl + '/Book'

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  constructor(private http:HttpClient) { }

  getAllBooks(): Observable<Book[]>
  {
    return this.http.get<Book[]>(this.apiUrl);
  }

  getBook(bookId: number): Observable<Book> {
    return this.http.get<Book>(`${this.apiUrl}/${bookId}`)
  }

  getBooksByCategoryId(categoryId:number): Observable<Book[]>{
    return this.http.get<Book[]>(`${this.apiUrl}/category/${categoryId} `)
  }
  addBook(book: Book): Observable<Book>{
    return this.http.post<Book>(this.apiUrl, book, this.httpOptions);
  }

  updateBook(bookId: number, book:Book): Observable<Book> {
    return this.http.put<Book>(`${this.apiUrl}/${bookId}`, book, this.httpOptions);
  }


  deleteBook(bookId: number): Observable<Book> {
    return this.http.delete<Book>(`${this.apiUrl}/${bookId}`, this.httpOptions);
  }
}
