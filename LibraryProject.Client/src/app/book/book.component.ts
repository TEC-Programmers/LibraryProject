import { Component, OnInit } from '@angular/core';
import { Book } from '../_models/Book';
import { BookService } from '../_services/book.service';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.css']
})
export class BookComponent implements OnInit {

  books: Book[] = [];

  constructor(private bookService: BookService) { }


  ngOnInit(): void {
    this.bookService.getAllBooks()
    .subscribe(c => this.books = c);
  }


}
