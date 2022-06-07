import { Component, OnInit } from '@angular/core';
import { Category } from '../_models/Category';
import { CategoryService } from '../_services/category.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {
  categories: Category[]=[];    //declare a Category type array
  category:Category = {id: 0, categoryName :""};   //Initialize the properties of the Category
  constructor(private categoryService: CategoryService ) { }  //Dependency Injection

  ngOnInit(): void {
    this.categoryService.getCategoriesWithoutBooks().subscribe(x => this.categories = x);   //calling function from the service and getting all categories and subscribe these. 
                                                                                            //So that other component can see and use that
    console.log('value received ' );
  }
  


}
