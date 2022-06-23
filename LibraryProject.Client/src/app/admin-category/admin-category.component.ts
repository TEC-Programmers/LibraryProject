import { Component, OnInit } from '@angular/core';
import { Category } from 'app/_models/Category';
import { CategoryService } from 'app/_services/category.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-admin-category',
  templateUrl: './admin-category.component.html',
  styleUrls: ['./admin-category.component.css']
})
export class AdminCategoryComponent implements OnInit {
  searchText!: string;
  message!: string;
  categorys: Category[] = [];
  category: Category = { id: 0, categoryName: '' }
  p: any;

  constructor(private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.categoryService.getAllCategories().subscribe(c => this.categorys = c);
  }

  cancel(): void {
    this.category = { id: 0, categoryName: '' }
  }

  delete(category: Category): void {
    if (confirm('Delete category: '+category.categoryName+'?')) {
      this.categoryService.deleteCategory(category.id)
      .subscribe(() => {
        this.categorys = this.categorys.filter(cus => cus.id != category.id)
      })
    }
  }

  edit(category: Category): void {
    this.message = '';
    this.category = category;
    this.category.id = category.id || 0;
    console.log(this.category);
  }

  save(): void {
    console.log(this.category)
    this.message = '';

    if(this.category.id == 0) {
      this.categoryService.addCategory(this.category)
      .subscribe({
        next: (x) => {
          this.categorys.push(x);
          this.category = { id: 0, categoryName: ''}
          this.message = '';
          Swal.fire({
            title: 'Success!',
            text: 'Category added successfully',
            icon: 'success',
            confirmButtonText: 'Continue'
          });
        },
        error: (err) => {
          console.log(err.error);
          this.message = Object.values(err.error.errors).join(", ");
        }
      });
    } else {
      this.categoryService.updateCategory(this.category.id, this.category)
      .subscribe({
        error: (err) => {
          console.log(err.error);
          this.message = Object.values(err.error.errors).join(", ");
        },
        complete: () => {
          this.message = '';
          this.category = { id: 0, categoryName: ''}
          Swal.fire({
            title: 'Success!',
            text: 'Category updated successfully',
            icon: 'success',
            confirmButtonText: 'Continue'
          });
        }
      });
    }
  }
}
