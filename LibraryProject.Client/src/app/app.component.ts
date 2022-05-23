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
    Id: 0,
    CategoryName: '',
    Books: []
  }

  author: Author[] = []

  authors: Author = {
    id: 0,
    firstName: '',
    lastName: ''
  }

  public Categories = ["børnebøger","voksenbøger", "Manga",]


  constructor() {}

  ngOnInit(): void {
    this.authors =  {
      id: 1,
      firstName: 'Bilal',
      lastName: '16'
    },
    {
      id: 2,
      firstName: 'Azam',
      lastName: '18'
    },
    {
      id: 3,
      firstName: 'Mahmood',
      lastName: '22'
    };
  }

}
