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
  categoryId:number=0;
  private sub: any;
  books:Book[]=[];
  constructor( private bookService:BookService, private route:ActivatedRoute) { }

  ngOnInit(): void {
    this.sub = this.route.params.subscribe(params => {
      this.categoryId = +params['id'];
      console.log("getting param");
      this.bookService.getBooksByCategoryId(this.categoryId).subscribe(x=> this.books=x);
    });
  }

}
