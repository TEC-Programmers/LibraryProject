import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Book } from '../_models/Book';
import { BookService } from '../_services/book.service';
import { AuthService } from '../_services/auth.service';
import { LoanService } from 'app/_services/loan.service';
import { Loan } from 'app/_models/Loan';
import { ReservationService } from 'app/_services/reservation.service';
import { Reservation } from 'app/_models/Reservation';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.css']
})
export class BookDetailsComponent implements OnInit {

  bookId: number = 0;
  book:Book = { id: 0, title: "", description: "", language: "", image: "",publishYear:0, authorId:0, categoryId:0,publisherId:0, author:{id:0,firstName:"",lastName:""} , publisher: {id:0, name:""}};
  isDisabled_loanBtn = false;
  isDisabled_reserveBtn = true;
  IdFound!: any;
  total_loans: Loan[] = [];
  loans: Loan[] = [];
  reservations: Reservation[] = [];
  // book_found: any = {};
  loanObj: Loan = { id: 0, userId: 0, bookId: 0, loaned_At: '', return_date: '' }

  constructor(private reserveService: ReservationService, private bookService:BookService, private route:ActivatedRoute, private router: Router, private authService: AuthService, private loanService: LoanService ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.bookId = +params['id'];
    });
    this.bookService.getBookById(this.bookId).subscribe(x=>
      { this.book=x,
      console.log('book-details on load: ',this.book);
    });

      this.findBookInLoan();
      this.findBookInReserve();

  }

  findBookInLoan(): void {
    console.log('clicked book: ',this.bookId)
    this.loanService.getAllLoans().subscribe(loan => {
      this.loans = loan;

      for (const key in this.loans) {
        if (this.loans.hasOwnProperty(key)) {
          // console.log(`${key} : ${this.loans[key]}`)

          if (this.loans[key].bookId == this.bookId) {
            this.isDisabled_loanBtn = true;
            this.isDisabled_reserveBtn = false;
            console.log('loan book found!  bookId: ',this.bookId)
          }
          else {
            console.log('Book not found')
          }
        }
      }
    });
  }

  findBookInReserve(): void {
      this.reserveService.getAllReservations().subscribe(res => {
        this.reservations = res;
        if (this.isDisabled_loanBtn == true) {
          for (const key in this.reservations) {
            if (this.reservations.hasOwnProperty(key)) {
              if (this.reservations[key].bookId == this.bookId) {
                this.isDisabled_reserveBtn = true;
                console.log('reservation book found!  bookId: ',this.bookId)
              }
              else {
                this.isDisabled_reserveBtn = true;
                this.isDisabled_loanBtn = false;
              }
            }
          }
        }     
      })
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
