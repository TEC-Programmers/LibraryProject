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
    return this.http.get<User[]>(this.apiUrl + `/getAll`);
  }

  getUser(id: number): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/WithProcedure/${id}`)
  }

  registerWithProcedure(user: User): Observable<User>{
    return this.http.post<User>(this.apiUrl + `/WithProcedure`, user, this.httpOptions);
  }

  updateProfileWithProcedure(id: number, user:User): Observable<User> {
    return this.http.put<User>(`${this.apiUrl}/updateUserProfile/${id}`, user, this.httpOptions);
  }

  updateRole(id: number, user: User): Observable<User> {
    return this.http.put<User>(`${this.apiUrl}/updateUserRole/${id}`, user, this.httpOptions);
  }

  updatePasswordWithProcedure(id: number, user: User): Observable<User> {
    return this.http.put<User>(`${this.apiUrl}/updateUserPassword/${id}`, user, this.httpOptions);
  }


  deleteUser(id: number): Observable<User> {
    return this.http.delete<User>(`${this.apiUrl}/WithProcedure/${id}`, this.httpOptions);
  }

}
