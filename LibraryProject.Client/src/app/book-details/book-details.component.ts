import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Book } from '../_models/Book';
import { BookService } from '../_services/book.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.css']
})
export class BookDetailsComponent implements OnInit {

  bookId: number = 0;

  book:Book = { id: 0, title: "", description: "", language: "", image: "",publishYear:0, authorId:0, categoryId:0,publisherId:0, author:{id:0,firstName:"",lastName:""} , publisher: {id:0, name:""}};
  constructor(private bookService:BookService, private route:ActivatedRoute, private router: Router, private authService: AuthService ) { }

  ngOnInit(): void {

    this.route.params.subscribe(params => {
      this.bookId = +params['id'];
    });
    this.bookService.getBookById(this.bookId).subscribe(x=>
      { this.book=x,

      console.log('book-details on load: ',this.book);
    });
  }

  loan(book:Book){
    if (this.authService.currentUserValue == null || this.authService.currentUserValue.id == 0) {
      alert("Do you have an account? If so, press ok and proceed, othwerwise make a new account by using the register button from the navigation bar");
      this.router.navigate(['login']);
    }
    else
    this.bookId = this.book.id;
    console.log('book-details: bookId: ',this.bookId)
    this.router.navigate(['/loan',this.bookId]);
  }


  reserve(book:Book){


  }
}
