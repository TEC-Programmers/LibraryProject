import { Component, OnInit } from '@angular/core';
import { environment } from '../../environments/environment';
import { Author } from '../_models/Author';
import { Book } from '../_models/Book';
import { Category } from '../_models/Category';
import { Loan } from '../_models/Loan';
import { AuthorService } from '../_services/author.service';
import { BookService } from '../_services/book.service';
import { LoanService } from '../_services/loan.service';

@Component({
  selector: 'app-frontpage',
  templateUrl: './frontpage.component.html',
  styleUrls: ['./frontpage.component.css']
})
export class FrontpageComponent implements OnInit {




  loans: Loan [] = [];
  loan: Loan = {id: 0, userID: 0, bookId: 0,  loaned_At: "", return_date: ""};

  constructor(private authorService: AuthorService, private loanService: LoanService, private bookservice: BookService) { }

  ngOnInit(): void {
    this.loanService.getAllLoans()
    .subscribe(p => this.loans = p);

  }

}
