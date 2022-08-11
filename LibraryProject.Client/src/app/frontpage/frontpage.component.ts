import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { environment } from '../../environments/environment';
import { Author } from '../_models/Author';
import { Book } from '../_models/Book';
import { Loan } from '../_models/Loan';
import { AuthorService } from '../_services/author.service';
import { LoanService } from '../_services/loan.service';
import { BookService } from '../_services/book.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { CategoryService } from '../_services/category.service';
import { Category } from 'app/_models/Category';
import { User } from 'app/_models/User';
import { AuthService } from 'app/_services/auth.service';
import { UserService } from 'app/_services/user.service';


@Component({
  selector: 'app-frontpage',
  templateUrl: './frontpage.component.html',
  styleUrls: ['./frontpage.component.css']
})
export class FrontpageComponent implements OnInit {

  title = 'LibraryProject-Client';

 // book!: Book;
  counter = 0;
  total: number = 0;
  categories: Category[] = [];
  allBooks: Book[] = [];
  filterTerm!: string;
  currentUser: User ={ id: 0, firstName: '', middleName: '', lastName: '', email: '', password: '', role: 0};
  x:any;




  book: Book = {

    id: 0, title: "", publishYear: 0, description: "", image: "", publisherId: 0, categoryId: 0,
    language: '',
    authorId: 0,
    author: {
      id: 0,
      firstName: '',
      lastName: ''
    },
    publisher: {
      id: 0,
      name: ''
    }
  }
  books: Book[] = [];
  bookId: number = 0;
  public searchTerm: string = "";
  searchBooks: Book[] = [];





    constructor(private bookService: BookService,
      private categoryService: CategoryService,
       private route: ActivatedRoute,
       private router: Router,
        private ref: ChangeDetectorRef,
         private authService: AuthService,
         private userService: UserService) {

  }

  ngOnInit(): void {

    this.route.params.subscribe(params => {
      if (params['filterTerm']) {
        this.bookService.getAllBooks().subscribe(x => {
          this.books = x;
          this.searchBooks = this.books;

          this.categoryService.getCategoriesWithoutBooks().subscribe(x => this.categories = x);
          this.bookService.search.subscribe((value: string) => {

            this.filterTerm = value
            console.log(this.searchBooks, this.books);
            this.searchBooks = this.books.filter(x =>
              x.title.toLowerCase().includes(this.filterTerm.toLowerCase()) || x.description.toLowerCase().includes(this.filterTerm.toLowerCase())
            )
          });
        });

      }
      else {
        this.bookService.getAllBooks().subscribe(x => {
          this.books = x;
          this.searchBooks = this.books;
        });
      }
    })

  }


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



ngAfterContentChecked(): void {
  this.ref.detectChanges();
  this.showOrhideAdminBtn();
  }
}
