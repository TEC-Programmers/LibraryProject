import { Component, OnInit } from '@angular/core';
import { Category } from '../_models/Category';
import { CategoryService } from '../_services/category.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {
  categories: Category[]=[];
  category:Category = {id: 0, categoryName :""};
  constructor(private categoryService: CategoryService ) { }

  ngOnInit(): void {
    this.categoryService.getCategoriesWithoutBooks().subscribe(x => this.categories = x);
    console.log('value received ', );
  }
  


}
