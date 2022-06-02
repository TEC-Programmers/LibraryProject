import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { Loan } from '../_models/Loan';
import { User } from '../_models/User';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private apiUrl = environment.apiUrl + '/User'

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  constructor(private http:HttpClient) { }


  getAllUsers(): Observable<User[]>
  {
    return this.http.get<User[]>(this.apiUrl);
  }

  getUserById(userId: number): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/${userId}`)
  }

  registerUser(user: User): Observable<User>{
    return this.http.post<User>(this.apiUrl, user, this.httpOptions);
  }

  updateUser(userId: number, user:User): Observable<User> {
    return this.http.put<User>(`${this.apiUrl}/${userId}`, user, this.httpOptions);
  }


  deleteUser(userId: number): Observable<User> {
    return this.http.delete<User>(`${this.apiUrl}/${userId}`, this.httpOptions);
  }


}
