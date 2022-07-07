import { Component } from '@angular/core';
import { Router}  from '@angular/router';
import { AuthService } from './_services/auth.service';
import {User } from './_models/User';
import {HostBinding } from '@angular/core';
import { Book } from './_models/Book';
import { Category } from './_models/Category';
import { BookService } from './_services/book.service';
import { CategoryService } from './_services/category.service';
import { of } from 'rxjs/internal/observable/of';
import { BehaviorSubject } from 'rxjs';
import { UserService } from './_services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {

  currentUser: User ={ id: 0, firstName: '', middleName: '', lastName: '', email: '', password: '', role: 0};

  title = 'LibraryProject-Client';

    book!: Book;
    counter = 0;
    total: number = 0;
    categories: Category[] = [];
    allBooks: Book[] = [];
    filterTerm!: string;  
    x: any;
  constructor(
    private router: Router,
      public authService: AuthService,
      private bookService: BookService,
      private categoryService: CategoryService,
      private userService: UserService

  ) {
    // get the current user from authentication service
    this.authService.currentUser.subscribe(x => this.currentUser = x);
    // console.log('user role: ',this.currentUser.role)
  }

  logout() {
    if (confirm('Are you sure you want to log out?')) {
      this.userService.getRole_(0);      
      // ask authentication service to perform logout
      this.authService.logout();

      // subscribe to the changes in currentUser, and load Home component
      this.authService.currentUser.subscribe(x => {
        this.currentUser = x
        this.router.navigate(['Login']);
      });
    }
    else {
      if (this.x === 1) {
        this.router.navigate(['Admin']);
      }
      else {
        this.router.navigate(['Frontpage']);
      }
    }
  }


  ngOnInit(): void {
    this.categoryService.getAllCategories()
    .subscribe(c => this.categories = c);

    this.showOrhideAdminBtn(); 
  }

  ngOnDestroy() {
    this.userService.getRole.unsubscribe();  
  }

  
showOrhideAdminBtn() {
  this.authService.currentUser.subscribe(x => {
  this.currentUser = x;

  if (this.currentUser) {
      if (this.currentUser.role.toString() === 'Administrator') {
          this.userService.getRole$.subscribe(x => this.x = x ); // start listening for changes 
      }
      else {
        this.userService.getRole_(0);
      }
    }
    else {
      this.userService.getRole_(0);
    } 
  });
}


showSearch(): void {
    if (this.filterTerm == null || this.filterTerm == '') {
     alert("The input field is empty")
    }
    else if (this.filterTerm.length >= 0 ) {
      this.bookService.getAllBooks()
    .subscribe(p => this.allBooks = p);
    console.log(this.allBooks)
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
