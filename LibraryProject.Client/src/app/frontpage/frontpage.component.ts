import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';
import { Book } from '../_models/Book';
import { Category } from '../_models/Category';
import { User } from '../_models/User';
import { BookService } from '../_services/book.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Ng2SearchPipeModule } from 'ng2-search-filter';


@Component({
  selector: 'app-frontpage',
  templateUrl: './frontpage.component.html',
  styleUrls: ['./frontpage.component.css']
})
export class FrontpageComponent implements OnInit {
  
  allBooks: Book[] = [];


  constructor(private bookService: BookService,
    private categoryService: CategoryService,  
    private router: Router) {    
    }




  constructor(private bookService: BookService) { }

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
    })




  }
  ngAfterContentChecked(): void {
    this.ref.detectChanges();
    this.showOrhideAdminBtn();  
  }

  }


