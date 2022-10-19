import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { Loan } from '../_models/Loan';

@Injectable({
  providedIn: 'root'
})
export class LoanService {

  private apiUrl = environment.apiUrl + '/Loan'

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  constructor(private http:HttpClient) { }

  getLoanById(loanId: number): Observable<Loan> {                 //Method for getting one specific Category by ID using API
    return this.http.get<Loan>(`${this.apiUrl}/${loanId}`)
  }

  getAllLoans(): Observable<Loan[]> {
    return this.http.get<Loan[]>(this.apiUrl);
  }

   addLoan(loan: Loan ): Observable<Loan>{
    return this.http.post<Loan>(this.apiUrl + `/WithProcedure`, loan, this.httpOptions);
   }

   updateLoan(loanId: number, loan: Loan ): Observable<Loan>{
    return this.http.put<Loan>(`${this.apiUrl}/${loanId}`, this.httpOptions);
   }

   DeleteLoan(loanId: number): Observable<Loan>{
    return this.http.delete<Loan>(`${this.apiUrl}/WithProcedure/${loanId}`, this.httpOptions);
   }
   
  }
