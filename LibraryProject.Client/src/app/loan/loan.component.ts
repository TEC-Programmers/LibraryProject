import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BookService } from '../_services/book.service';
import { CategoryService } from '../_services/category.service';
import { DarkModeService } from 'angular-dark-mode';
import { Observable } from 'rxjs';
import { LoanService } from '../_services/loan.service';
import { Loan } from '../_models/Loan';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { environment } from '../../environments/environment';



@Component({
  selector: 'app-loan',
  templateUrl: './loan.component.html',

  styleUrls: ['./loan.component.css']
})
export class LoanComponent implements OnInit {
  dateRangeForm!: FormGroup;
  loans: Loan[] = [];

private apiUrl = environment.apiUrl + '/Loan';

private httpOptions = {
  headers: new HttpHeaders({'Content-Type' : 'application/json'})

};
  constructor(private bookService: BookService, private categoryService: CategoryService,  private formBuilder: FormBuilder, private loanservice: LoanService, private http:HttpClient ) {}
  range = new FormGroup({
    fromDate: new FormControl('', Validators.required),
    toDate: new FormControl('', Validators.required)
  });

  ngOnInit(): void {
    this.dateRangeForm = this.formBuilder.group({
      fromDate: new FormControl('', Validators.required),
      toDate: new FormControl('', Validators.required),
    });
    this.loanservice.getAllLoans()
        .subscribe(x => this.loans = x);

  }
  onFormSubmit() {
    console.log('Is Form Invalid', this.dateRangeForm.invalid);
  }

getAllLoans(): Observable<Loan[]> {
  return this.http.get<Loan[]>(this.apiUrl)
}
getLoan(loanId: number): Observable<Loan[]> {
  return this.http.get<Loan[]>(`${this.apiUrl}/${loanId}`);
}
addLoan(loan: Loan): Observable<Loan[]> {
  return this.http.post<Loan[]>(this.apiUrl, loan, this.httpOptions);
}
updateLoan(loanId: Number): Observable<Loan[]> {
  return this.http.put<Loan[]>(`${this.apiUrl}/${loanId}`, this.httpOptions);
}
deleteLoan(loanId: Number): Observable<Loan[]> {
  return this.http.delete<Loan[]>(`${this.apiUrl}/${loanId}`, this.httpOptions);
}
}
