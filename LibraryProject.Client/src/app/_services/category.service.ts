import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Category } from '../_models/Category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private apiUrl = environment.apiUrl + '/Category'

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  constructor(private http:HttpClient) { }


  getAllCategories(): Observable<Category[]>
  {
    return this.http.get<Category[]>(this.apiUrl);
  }

  getCategoryById(categoryId: number): Observable<Category> {
    return this.http.get<Category>(`${this.apiUrl}/${categoryId}`)
  }
  getCategoriesWithoutBooks(): Observable<Category[]>{
    return this.http.get<Category[]>(`${this.apiUrl}/WithoutBooks`)
 }

  addCategory(category: Category): Observable<Category>{
    return this.http.post<Category>(this.apiUrl, category, this.httpOptions);
  }

  updateCategory(categoryId: number, Category:Category): Observable<Category> {
    return this.http.put<Category>(`${this.apiUrl}/${categoryId}`, Category, this.httpOptions);
  }

  deleteAuthor(categoryId: number): Observable<Category> {
    return this.http.delete<Category>(`${this.apiUrl}/${categoryId}`, this.httpOptions);
  }

  
}
