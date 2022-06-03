import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Category } from '../_models/Category';
import { CategoryService } from '../_services/category.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {

  id: number = 0;
  Categories: Category[] = [];
  // Category: Category = {
  //   id: 0,
  //   CategoryName: '',
  //   books: []
  // }
  category!: Category;




  constructor(private categoryService: CategoryService, private _Activatedroute:ActivatedRoute, private _router:Router) { }

  ngOnInit(): void{

    this.categoryService.getAllCategories()
    .subscribe(c => this.Categories = c);

    // this.categoryService.addCategory()
    // .subscribe(c => this.Categories = c);
}}
