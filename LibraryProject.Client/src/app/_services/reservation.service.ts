import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Reservation } from '../_models/Reservation';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {

  private apiUrl = environment.apiUrl + '/Reservation'

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  constructor(private http:HttpClient) { }

  getAllReservations(): Observable<Reservation[]>
  {
    return this.http.get<Reservation[]>(this.apiUrl);
  }

  getReservationById(reservationId: number): Observable<Reservation> {
    return this.http.get<Reservation>(`${this.apiUrl}/${reservationId}`)
  }

  addReservation(reservation: Reservation): Observable<Reservation>{
    return this.http.post<Reservation>(this.apiUrl + `WithProcedure`, reservation, this.httpOptions);
  }

  updateReservation(reservationId: number, Reservation:Reservation): Observable<Reservation> {
    return this.http.put<Reservation>(`${this.apiUrl}/${reservationId}`, Reservation, this.httpOptions);
  }


  deleteReservation(reservationId: number): Observable<Reservation> {
    return this.http.delete<Reservation>(`${this.apiUrl}/WithProcedure/${reservationId}`, this.httpOptions);
  }

  
}
