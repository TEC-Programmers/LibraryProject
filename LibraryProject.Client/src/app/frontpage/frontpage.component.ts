import { Component, OnInit } from '@angular/core';
import { environment } from '../../environments/environment';
import { Author } from '../_models/Author';
import { Book } from '../_models/Book';
import { Loan } from '../_models/Loan';
import { AuthorService } from '../_services/author.service';
import { LoanService } from '../_services/loan.service';
import { BookService } from '../_services/book.service';


@Component({
  selector: 'app-frontpage',
  templateUrl: './frontpage.component.html',
  styleUrls: ['./frontpage.component.css']
})
export class FrontpageComponent implements OnInit {
  
  allBooks: Book[] = [];

books : Book[]= [];





  constructor(private bookService: BookService) { }

  ngOnInit(): void {
  }

}
