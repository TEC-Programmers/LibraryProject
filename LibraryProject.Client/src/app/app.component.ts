import { Component } from '@angular/core';
import { CategoryComponent } from './category/category.component';
import { Category } from './_models/Category';
import { CategoryService } from './_services/category.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'LibraryProject-Client';



  categories: Category[] = [];



  constructor(private categoryService:CategoryService) {}

  ngOnInit(): void {
    this.categoryService.getAllCategories()
    .subscribe(c => this.categories = c);

  }

}
