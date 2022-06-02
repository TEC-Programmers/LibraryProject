import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Book } from '../_models/Book';
import { Category } from '../_models/Category';
import { Publisher } from '../_models/Publisher';
import { BookService } from '../_services/book.service';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.css']
})
export class BookDetailsComponent implements OnInit {

  id:number = 0
  Book!: Book;
  Books: Book[] = [];
  category: Category[] = []
  publishers: Publisher[] = [];
  
  constructor(private bookService: BookService, private _Activatedroute:ActivatedRoute) { }

  ngOnInit(): void {

    this._Activatedroute.paramMap.subscribe(params =>
      {
        this.id = parseInt(params.get('id') || '0');
      this.bookService.getBookById(this.id)
      .subscribe(c => this.Book = c);
    });
  }

}
