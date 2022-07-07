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
  }
  ngOnInit(): void {
    // this.categoryService.getAllCategories()
    // .subscribe(c => this.categories = c);

    this.categoryService.getCategoriesWithoutBooks().subscribe(x => this.categories = x);

    this.route.params.subscribe(params => {
      if (params['searchTerm'])
        this.searchTerm = params['searchTerm'];
    })
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

  //   pageYoffset = 0;
  //   @HostListener('window:scroll', ['$event']) onScroll(event){
  //     this.pageYoffset = window.pageYOffset;
  //  }
  // @ViewChild('scroll')
  // scroll!: ElementRef;

  // categories: Category[] = [];





  /* scrollToTop(){
   // this.scroll.scrollToPosition([0,0]);
   this.scroll.nativeElement.scrollToTop = 0;
 }
 
 scrollToBottom(){
   console.log(this.scroll.nativeElement.scrollHeight)
   this.scroll.nativeElement.scrollToTop = this.scroll.nativeElement.scrollHeight;
 } */


  // searchKey(event:any){
  //   this.searchTerm=(event.target as HTMLInputElement).value;
  //   console.log(this.searchTerm);
  //   this.bookService.search.next(this.searchTerm);
  // } 
}
