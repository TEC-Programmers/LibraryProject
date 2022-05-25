import { Component, OnInit } from '@angular/core';
import { environment } from '../../environments/environment';
import { Author } from '../_models/Author';
import { Category } from '../_models/Category';
import { Loan } from '../_models/Loan';
import { AuthorService } from '../_services/author.service';
import { CategoryService } from '../_services/category.service';
import { LoanService } from '../_services/loan.service';

@Component({
  selector: 'app-frontpage',
  templateUrl: './frontpage.component.html',
  styleUrls: ['./frontpage.component.css']
})
export class FrontpageComponent implements OnInit {

  authors: Author [] = [];

  categories: Category[]=[];
  category:Category = {Id: 0, CategoryName :""};

  constructor(private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.categoryService.getCategoriesWithoutBooks().subscribe(x => this.categories = x);
    console.log('value received ', );

  }

}
