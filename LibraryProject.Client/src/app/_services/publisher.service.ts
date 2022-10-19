import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Publisher } from '../_models/Publisher';

@Injectable({
  providedIn: 'root'
})
export class PublisherService {

  private apiUrl = environment.apiUrl + '/Publisher'

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  constructor(private http:HttpClient) { }

  getAllPublishers(): Observable<Publisher[]>
  {
    return this.http.get<Publisher[]>(this.apiUrl);
  }

  getPublisherById(publisherId: number): Observable<Publisher> {
    return this.http.get<Publisher>(`${this.apiUrl}/${publisherId}`)
  }

  addPublisher(publisher: Publisher): Observable<Publisher>{
    return this.http.post<Publisher>(this.apiUrl + `/WithProcedure`, publisher, this.httpOptions);
  }

  updatePublisher(publisherId: number, publisher:Publisher): Observable<Publisher> {
    return this.http.put<Publisher>(`${this.apiUrl}/${publisherId}`, publisher, this.httpOptions);
  }

  deletePublisher(publisherId: number): Observable<Publisher> {
    return this.http.delete<Publisher>(`${this.apiUrl}/WithProcedure/${publisherId}`, this.httpOptions);
  }
}
