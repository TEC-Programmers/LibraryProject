import { Component, OnInit } from '@angular/core';
import { environment } from '../../environments/environment';
import { Author } from '../_models/Author';
import { Book } from '../_models/Book';
import { Loan } from '../_models/Loan';
import { AuthorService } from '../_services/author.service';
import { LoanService } from '../_services/loan.service';
import { BookService } from '../_services/book.service';
import { ActivatedRoute, Router } from '@angular/router';


@Component({
  selector: 'app-frontpage',
  templateUrl: './frontpage.component.html',
  styleUrls: ['./frontpage.component.css']
})
export class FrontpageComponent implements OnInit {
  
  book: Book = {
    id: 0, title: "", publishYear: 0, description: "", image: "", publisherId: 0, categoryId: 0,
    language: '',
    authorId: 0,
    author: {
      id: 0,
      firstName: '',
      lastName: ''
    },
    publisher: {
      id: 0,
      name: ''
    }
  }
  books: Book[] = [];
  bookId: number = 0;
  searchKey: string = "";
  searchBooks: Book[] = [];





  constructor(private bookService: BookService,  private route: ActivatedRoute, private router: Router) { }

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
