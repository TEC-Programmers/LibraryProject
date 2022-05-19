import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment.prod';
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

  constructor(private authorService: AuthorService, private loanService: LoanService) { }

  ngOnInit(): void {

  }

}
