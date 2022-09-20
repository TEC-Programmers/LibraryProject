import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { BehaviorSubject, Observable } from 'rxjs';
import { Loan } from '../_models/Loan';
import { User } from '../_models/User';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private apiUrl = environment.apiUrl + '/user'

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  constructor(private http:HttpClient) { }

  public getRole: BehaviorSubject<number> = new BehaviorSubject(0);
  public getRole$: Observable<number> = this.getRole.asObservable();

  getRole_(roleNr: number) {
    this.getRole.next(roleNr)
  }

  getAllUsers(): Observable<User[]>
  {
    return this.http.get<User[]>(this.apiUrl);
  }

  getUser(id: number): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/${id}`)
  }

  registerUser(user: User): Observable<User>{
    return this.http.post<User>(this.apiUrl + `/registerWithProcedure`, user, this.httpOptions);
  }

  updateUser(id: number, user:User): Observable<User> {
    return this.http.put<User>(`${this.apiUrl}/${id}`, user, this.httpOptions);
  }

  updateRole(id: number, user:User): Observable<User> {
    return this.http.put<User>(`${this.apiUrl}/role/${id}`, user, this.httpOptions);
  }


  deleteUser(id: number): Observable<User> {
    return this.http.delete<User>(`${this.apiUrl}/${id}`, this.httpOptions);
  }


}
