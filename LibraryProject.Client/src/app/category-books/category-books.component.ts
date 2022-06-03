import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Book } from '../_models/Book';
import { BookService } from '../_services/book.service';

@Component({
  selector: 'app-category-books',
  templateUrl: './category-books.component.html',
  styleUrls: ['./category-books.component.css']
})
export class CategoryBooksComponent implements OnInit {
  categoryId:number=0; //declare and initialize a variable
  private sub: any;
  books:Book[]=[];
  constructor( private bookService:BookService, private route:ActivatedRoute) { }    //Dependency Injection


  ngOnInit(): void {
    this.sub = this.route.params.subscribe(params => {    //retrieving the value of id by subscribing to the params observable property 
      this.categoryId = +params['id'];  // (+) converts string 'id' to a number
      console.log("getting param");
      this.bookService.getBooksByCategoryId(this.categoryId).subscribe(x=> this.books=x);   //retrieving all of the books of the specific category and subscribing that for being observable
    });
  }

}
