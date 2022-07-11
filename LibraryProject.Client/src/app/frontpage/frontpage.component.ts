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
import { AuthService } from 'app/_services/auth.service';
import { UserService } from 'app/_services/user.service';
import { User } from 'app/_models/User';


@Component({
  selector: 'app-frontpage',
  templateUrl: './frontpage.component.html',
  styleUrls: ['./frontpage.component.css']
})
export class FrontpageComponent implements OnInit {
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
  public filterTerm: string = "";
  searchBooks: Book[] = [];





    constructor(private bookService: BookService, private route: ActivatedRoute, private router: Router, private ref: ChangeDetectorRef, private authService: AuthService, private userService: UserService) {

  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      if (params['filterTerm']) {
        this.bookService.getAllBooks().subscribe(x => {
          this.books = x;
          this.searchBooks = this.books;


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


  showOrhideAdminBtn() {
    this.authService.currentUser.subscribe(user => {
    this.currentUser = user;
  
    if (this.x !== 1) {
      if (this.currentUser) {
        this.userService.getRole$.subscribe(x => this.x = x); // start listening for changes 
          if (this.currentUser.role.toString() === 'Administrator') {
            this.userService.getRole_(1);
          }
          else {
            this.userService.getRole_(0);
          }
        }
        else {
          this.userService.getRole_(0);
        } 
    }
    });
  }




  
  ngAfterContentChecked(): void {
    this.ref.detectChanges();
    this.showOrhideAdminBtn();  
  }

}
