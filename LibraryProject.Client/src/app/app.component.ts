import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Category } from './_models/Category';
import { CategoryService } from './_services/category.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'LibraryProject-Client';

  Categories: Category[] = [];

  category: Category = {
    Id: 1,
    CategoryName: 'Manga',
    Books: []
  }


  constructor(private categoryService: CategoryService) {}

  ngOnInit(): void {

    this.categoryService.getAllCategories()
    .subscribe(c => this.Categories = c);
  }

}
