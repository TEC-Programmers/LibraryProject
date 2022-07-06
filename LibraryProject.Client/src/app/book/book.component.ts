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
  searchKey: string = "";
  searchBooks: Book[] = [];
  constructor(private bookService: BookService) { }


  ngOnInit(): void {
    this.bookService.getAllBooks().subscribe(x =>{
      this.books = x;
      this.searchBooks=this.books;


      this.bookService.search.subscribe((value: string) => {

        this.searchKey = value
        console.log(this.searchBooks, this.books);
        this.searchBooks = this.books.filter(x =>
          x.title.toLowerCase().includes(this.searchKey.toLowerCase()) || x.description.toLowerCase().includes(this.searchKey.toLowerCase())

      )});
    });
  }


}
