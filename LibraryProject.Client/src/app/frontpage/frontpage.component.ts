import { Component, OnInit } from '@angular/core';
import { environment } from '../../environments/environment';
import { Author } from '../_models/Author';
import { Loan } from '../_models/Loan';
import { AuthorService } from '../_services/author.service';
import { LoanService } from '../_services/loan.service';

@Component({
  selector: 'app-frontpage',
  templateUrl: './frontpage.component.html',
  styleUrls: ['./frontpage.component.css']
})
export class FrontpageComponent implements OnInit {

  authors: Author [] = [];

  loans: Loan [] = [];
  loan: Loan = {id: 0, userID: 0, bookId: 0,  loaned_At: "", return_date: ""};

  constructor(private authorService: AuthorService, private loanService: LoanService) { }

  ngOnInit(): void {
    this.loanService.getAllLoans()
    .subscribe(p => this.loans = p);

  }

}
