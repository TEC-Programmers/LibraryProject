import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Category, Book_ } from '../_models/Category';
import { BookService } from '../_services/book.service';
import { CategoryService } from '../_services/category.service';

@Component({
  selector: 'app-category-details',
  templateUrl: './category-details.component.html',
  styleUrls: ['./category-details.component.css']
})
export class CategoryDetailsComponent implements OnInit {

id: number = 0;
category!: Category;
categories: Category[] = [];
  books: Book_[] = [];

  constructor(private bookService:BookService, private categoryService: CategoryService, private _Activatedroute:ActivatedRoute, private _router:Router) { }

  ngOnInit(): void {
        //  gets clicked category and prints its products
         this._Activatedroute.paramMap.subscribe(params =>
          {
            this.id = parseInt(params.get('id') || '3');
          this.categoryService.getCategoryById(this.id)
          .subscribe(c => this.category = c);
        });

  this.bookService.getAllBooks()
  .subscribe(p => this.books = p);
  }
}
