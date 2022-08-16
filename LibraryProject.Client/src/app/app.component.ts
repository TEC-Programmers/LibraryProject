import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { Book } from './_models/Book';
import { Category } from './_models/Category';
import { User } from './_models/User';
import { AuthService } from './_services/auth.service';
import { BookService } from './_services/book.service';
import { CategoryService } from './_services/category.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
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
    private authService: AuthService,
    private router: Router)
    {
    this.authService.currentUser.subscribe(x => this.currentUser= x); // get the current user from authentication service
  }
  ngOnInit(): void {
    // console.log(this.currentUser)
    // this.categoryService.getAllCategories()
    // .subscribe(c => this.categories = c);
    console.log(this.currentUser.role)
    this.categoryService.getCategoriesWithoutBooks().subscribe(x => this.categories = x);

    console.log(this.categories)
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

    logout() {
      if (confirm('Are you sure you want to log out?')) {
        // ask authentication service to perform logout
        this.authService.logout();


        // subscribe to the changes in currentUser, and load Home component
        this.authService.currentUser.subscribe(x => {
          this.currentUser = x
          this.router.navigate(['/']);
        });
      }

    }




}
