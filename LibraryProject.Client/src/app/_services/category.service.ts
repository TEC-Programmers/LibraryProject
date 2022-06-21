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

  constructor(private http:HttpClient) { }  //Dependency Injection


  getAllCategories(): Observable<Category[]>        //Method for getting list of all categories with books details using API
  {
    return this.http.get<Category[]>(this.apiUrl);
  }

  getCategoryById(categoryId: number): Observable<Category> {                 //Method for getting one specific Category by ID using API
    return this.http.get<Category>(`${this.apiUrl}/${categoryId}`)
  }
  getCategoriesWithoutBooks(): Observable<Category[]>{                        //Method for getting list of categories without books details using API
    return this.http.get<Category[]>(`${this.apiUrl}/WithoutBooks`)
 }

  addCategory(category: Category): Observable<Category>{                  //Method for adding one Category in the database using API
    return this.http.post<Category>(this.apiUrl, category, this.httpOptions);
  }

  updateCategory(categoryId: number, Category:Category): Observable<Category> {           //Method for updating  category's info in the database using API
    return this.http.put<Category>(`${this.apiUrl}/${categoryId}`, Category, this.httpOptions);
  }

  deleteCategory(categoryId: number): Observable<Category> {                                        //Method for deleting one category from the database using API
    return this.http.delete<Category>(`${this.apiUrl}/${categoryId}`, this.httpOptions);
  }

  
}
