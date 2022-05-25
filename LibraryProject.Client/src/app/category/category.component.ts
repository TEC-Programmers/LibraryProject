import { Component, OnInit } from '@angular/core';
import { Category } from '../_models/Category';
import { CategoryService } from '../_services/category.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {


  categories: Category[] = []
  constructor(private CategoryService: CategoryService) { }

  ngOnInit(): void {
    this.CategoryService.getAllCategories()
    .subscribe(p => this.categories = p);

  }

}
