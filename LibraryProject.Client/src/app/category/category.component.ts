import { Component, OnInit } from '@angular/core';
import { Category } from '../_models/Category';
import { CategoryService } from '../_services/category.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {

  Categories: Category[] = [];

  // category: Category = {
  //   Id: 1,
  //   CategoryName: 'Manga',
  //   Books:[]
  // }

  public categories = ["børnebøger","voksenbøger", "Manga",]

  public childrensBooks = ["Tal for børn","Alfabet for Børn","Navne på ting; for børn"]



  constructor(private categoryService: CategoryService) { }

  ngOnInit(): void {

    // this.categoryService.getAllCategories()
    // .subscribe(c => this.Categories = c);
  }
}
