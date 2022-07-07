import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';
import { Book } from '../_models/Book';
import { Category } from '../_models/Category';
import { User } from '../_models/User';
import { BookService } from '../_services/book.service';
import { CategoryService } from '../_services/category.service';


@Component({
  selector: 'app-frontpage',
  templateUrl: './frontpage.component.html',
  styleUrls: ['./frontpage.component.css']
})
export class FrontpageComponent implements OnInit {
  
  title = 'LibraryProject-Client';

  book!: Book;
  counter = 0;
  total: number = 0;
  categories: Category[] = [];
  allBooks: Book[] = [];
  filterTerm!: string;
  currentUser: User ={ id: 0, firstName: '', middleName: '', lastName: '', email: '', password: '', role:0 };


  constructor(private bookService: BookService,
    private categoryService: CategoryService,  
    private router: Router) {    
    }

  ngOnInit(): void {
    this.categoryService.getCategoriesWithoutBooks().subscribe(x => this.categories = x);
  }
showSearch(): void {

    if (this.filterTerm == null || this.filterTerm == '') {
     alert("The input field is empty")
    }

    else if (this.filterTerm.length >= 0 ){
      this.bookService.getAllBooks()
    .subscribe(p => this.allBooks = p);
    console.log(this.allBooks);
    }
  }
  click(){
      if (this.filterTerm == null || this.filterTerm == '') {
        alert("The input field is empty")
       }

       else if (this.filterTerm.length >= 0 ){
         this.bookService.getAllBooks()
       .subscribe(p => this.allBooks = p);
       console.log(this.allBooks)
       }
    }
    checkSearch(event:any){
      if (event.key === "Backspace" || this.filterTerm == null) {
        this.allBooks = [];
        console.log(event);
      }
    }

  }


