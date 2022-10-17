import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Book } from '../_models/Book';
import { Category } from '../_models/Category';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  private apiUrl = environment.apiUrl + '/Book'

  public search = new BehaviorSubject<string>("");
  
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  constructor(private http:HttpClient) { }  //Dependency Injection

  getAllBooks(): Observable<Book[]>  //Method for getting list of Books using API
  {
    return this.http.get<Book[]>(this.apiUrl);  
  }

  getBookById(bookId: number): Observable<Book> {         //Method for getting one specific Book using API
    return this.http.get<Book>(`${this.apiUrl}/${bookId}`)
  }

  getBooksByCategoryId(categoryId:number): Observable<Book[]>{             //Method for getting list of Books by specific Category using API
    return this.http.get<Book[]>(`${this.apiUrl}/category/${categoryId} `)
  }
  addBook(book: Book): Observable<Book>{                                     //Method for adding one book in the database using API
    return this.http.post<Book>(this.apiUrl + `WithProcedure`, book, this.httpOptions);
  }

  updateBook(bookId: number, book:Book): Observable<Book> {                                        //Method for updating  book's info in the database using API
    return this.http.put<Book>(`${this.apiUrl}/${bookId}`, book, this.httpOptions);
  }


  deleteBook(bookId: number): Observable<Book> {                                       //Method for deleting one book from the database using API
    return this.http.delete<Book>(`${this.apiUrl}/${bookId}`, this.httpOptions);
  }
}
