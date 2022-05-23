import { Component } from '@angular/core';
import { CategoryComponent } from './category/category.component';
import { Author } from './_models/Author';
import { Category } from './_models/Category';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'LibraryProject-Client';

  category: Category[] = []

  categories: Category = {
    Id: 1,
    CategoryName: '',
    Books: []
  }



  constructor() {}

  ngOnInit(): void {

  }

}
