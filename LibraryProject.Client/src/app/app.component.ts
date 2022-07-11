import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
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
  searchBooks: Book[] = [];
  currentUser: User = { id: 0, firstName: '', middleName: '', lastName: '', email: '', password: '', role: 0 };
  public searchTerm: string = "";

  constructor(private bookService: BookService,
    private categoryService: CategoryService,
    private authService: AuthService,
    private router: Router, private route: ActivatedRoute) {   // get the current user from authentication service
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
    // this.categoryService.getAllCategories()
    // .subscribe(c => this.categories = c);

      this.categoryService.getCategoriesWithoutBooks().subscribe(x => this.categories = x);
      this.showOrhideAdminBtn();

    this.route.params.subscribe(params => {
      if (params['searchTerm'])
        this.searchTerm = params['searchTerm'];
    })
    }


    ngOnDestroy() {
        this.userService.getRole.unsubscribe();
    }


    showOrhideAdminBtn() {
        this.authService.currentUser.subscribe(x => {
            this.currentUser = x;

            if (this.currentUser) {
                if (this.currentUser.role.toString() === 'Administrator') {
                    this.userService.getRole$.subscribe(x => this.x = x); // start listening for changes 
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

  // search():void{

  //   if(this.searchTerm)
  //   this.router.navigate(['/Book', this.searchTerm]);
  // }

  /* showSearch(): void {
 
     if (this.filterTerm == null || this.filterTerm == '') {
      alert("The input field is empty")
     }
 
     else if (this.filterTerm.length >= 0 ){
       this.bookService.getAllBooks()
     .subscribe(p => this.allBooks = p);
     console.log(this.allBooks);
     this.router.navigate(['/Book', this.filterTerm]);
     }
 
 
   } */
  click() {
    if (this.filterTerm == null || this.filterTerm == '') {
      alert("The input field is empty")
    }

    else if (this.filterTerm.length >= 0) {
      this.bookService.getAllBooks()
        .subscribe(p => this.allBooks = p);
      console.log(this.allBooks);

      this.bookService.search.next(this.filterTerm);
      this.router.navigate(['/Book', this.filterTerm]);
    }
  }
  checkSearch(event: any) {
    if (event.key === "Enter" || this.filterTerm == null) {
      this.allBooks = [];
      this.searchTerm = (event.target as HTMLInputElement).value;
      console.log(event);
      this.bookService.search.next(this.filterTerm);
      this.router.navigate(['/Book', this.filterTerm]);
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



  // searchKey(event:any){
  //   this.searchTerm=(event.target as HTMLInputElement).value;
  //   console.log(this.searchTerm);
  //   this.bookService.search.next(this.searchTerm);
  // } 
}
