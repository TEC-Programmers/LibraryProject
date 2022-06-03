import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Book } from '../_models/Book';
import { BookService } from '../_services/book.service';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.css']
})
export class BookDetailsComponent implements OnInit {

  bookId: number =0;
  
  book:Book={id: 0, title: "", description: "", language: "", image: "",publishYear:0, authorId:0, categoryId:0,publisherId:0, author:{id:0,firstName:"",lastName:""} , publisher: {id:0, name:""}};
  constructor(private bookService:BookService, private route:ActivatedRoute) { }

  ngOnInit(): void {

    this.route.params.subscribe(params => {
      this.bookId = +params['id'];
    });
    this.bookService.getBookById(this.bookId).subscribe(x=> 
      { this.book=x,
      
      console.log(this.book);
    });
  }

  loan(book:Book){


  }
  reserve(book:Book){

    
  }
}
