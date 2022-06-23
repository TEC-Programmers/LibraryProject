import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Book } from '../_models/Book';
import { BookService } from '../_services/book.service';
import { AuthService } from '../_services/auth.service';
import { LoanService } from 'app/_services/loan.service';
import { Loan } from 'app/_models/Loan';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.css']
})
export class BookDetailsComponent implements OnInit {

  bookId: number = 0;
  book:Book = { id: 0, title: "", description: "", language: "", image: "",publishYear:0, authorId:0, categoryId:0,publisherId:0, author:{id:0,firstName:"",lastName:""} , publisher: {id:0, name:""}};
  isDisabled_loanBtn = false;
  IdFound!: any;
  total_loans: Loan[] = [];
  loans: Loan[] = [];
  // book_found: any = {};
  loanObj: Loan = { id: 0, userId: 0, bookId: 0, loaned_At: '', return_date: '' }

  constructor(private bookService:BookService, private route:ActivatedRoute, private router: Router, private authService: AuthService, private loanService: LoanService ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.bookId = +params['id'];
    });
    this.bookService.getBookById(this.bookId).subscribe(x=>
      { this.book=x,
      console.log('book-details on load: ',this.book);
    });

    console.log('clicked book: ',this.bookId)
    this.loanService.getAllLoans().subscribe(loan => {
      this.loans = loan;

      for (const key in this.loans) {
        if (this.loans.hasOwnProperty(key)) {
          console.log(`${key} : ${this.loans[key]}`)

          if (this.loans[key].bookId == this.bookId) {
            console.log('book found!  bookId: ',this.bookId)
            this.isDisabled_loanBtn = true;
          }
          else {
        console.log('Book not found')
          }
        }
      }
    });

    



    // this.loans = this.loans.filter((loan) => {
    //   if (loan.bookId === this.bookId) {
    //     console.log('book found!  bookId: ',this.bookId)
    //   }
    //   else {
    //     console.log('Book not found')
    //   }         
    // })

    // this.customers = this.total_users.filter((obj) => {
    //   return obj.role.toString() === key
    // });




    // for (let book of this.loans) {
    //   if (book.hasOwnProperty(this.bookId)) {
    //     console.log('book found!  bookId: ',this.bookId)
    //   }
    // }

    // this.IdFound = this.loans.filter((x) => x.bookId.includes(this.bookId))

    // this.IdFound = this.loans.find(x => x.bookId == this.bookId)
  }

  loan(book:Book){
    if (this.authService.currentUserValue == null || this.authService.currentUserValue.id == 0) {
      alert("Do you have any account? If yes, then Login, otherwise create a new account..");
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
