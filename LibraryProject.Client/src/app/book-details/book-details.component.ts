import { Component, ElementRef, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Book } from '../_models/Book';
import { BookService } from '../_services/book.service';
import { AuthService } from '../_services/auth.service';
import { LoanService } from 'app/_services/loan.service';
import { Loan } from 'app/_models/Loan';
import { ReservationService } from 'app/_services/reservation.service';
import { Reservation } from 'app/_models/Reservation';
import { User } from 'app/_models/User';
import { UserService } from 'app/_services/user.service';
import { filter } from 'rxjs';
import { DatePipe, formatDate } from '@angular/common';
import moment from 'moment';
import { ThisReceiver } from '@angular/compiler';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.css']
})
export class BookDetailsComponent implements OnInit {

  bookId: number = 0;
  book: Book = { id: 0, title: "", description: "", language: "", image: "",publishYear:0, authorId:0, categoryId:0,publisherId:0, author:{id:0,firstName:"",lastName:""} , publisher: {id:0, name:""}};
  isDisabled_loanBtn = false;
  isDisabled_reserveBtn = true;
  loan_found: boolean = false;
  total_loans: Loan[] = [];
  total_reservations: Reservation[] = [];
  loans: Loan[] | undefined = [];
  reservations: Reservation[] = [];
  loanObjFound: Loan = { id: 0, userId: 0, bookId: 0, loaned_At: '', return_date: '' }
  userId: number = 0;
  books: Book[] = [];
  users: User[] = []
  user: User = { id: 0, firstName: '', middleName: '' , lastName: '', email: '', password: '', role: 0, token: '' }
  userLoggedIn_status: boolean = false;
  bookBorrowed_status: boolean = false;
  bookAvailable_status: boolean = false;
  public userLoggedIn;
  public bookBorrowed;
  public userLoggedIn_res;
  public bookReserved;
  public convert_string_to_date;
  public getDate;
  dateToday = new Date();
  public outputDate;

  constructor(private _datePipe: DatePipe, private userService: UserService, private reserveService: ReservationService, private bookService:BookService, private route:ActivatedRoute, private router: Router, private authService: AuthService, private loanService: LoanService, private elementRef: ElementRef) { }

  ngOnInit(): void {
    this.elementRef.nativeElement.ownerDocument.body.style.backgroundColor = '#AFEEEE';
    this.bookService.getAllBooks().subscribe(b => this.books = b);
    this.userService.getAllUsers().subscribe(u => this.users = u)

    this.route.params.subscribe(params => {
      this.bookId = +params['id'];
    });

    this.bookService.getBookById(this.bookId).subscribe(x => { 
      this.book = x;
      // console.log('book-details on load: ',this.book);
    });
     
    this.checkIfLoanOrReservationExists();   
    this.checkStatus();
    this.deleteOutdatedLoans();
    this.deleteOutdatedReservations();
  }

  deleteOutdatedReservations() {
    this.reserveService.getAllReservations().subscribe({
      next: (all_reservations) => {
        this.total_reservations = all_reservations;

        if (this?.total_reservations) {
            for (let date of this.total_reservations) {
              // get book return date
              var storedDates = date.reserved_To;

              // convert return date to type Date
              var storedDates_converted = new Date(storedDates)
    
              if (this.dateToday > storedDates_converted) {
                var storedDates_reverted = formatDate(storedDates_converted, 'yyyy/MM/dd', 'en-US');
    
                // get all loans that has higher 'return_date' than current day today and then delete them.
                var getExpiredReservations = this.total_reservations.filter((reservation) => {
                return ((reservation["reserved_To"] == storedDates_reverted))
              })
    
                // Delete All Outdated reservation's
                this.reserveService.deleteReservation(getExpiredReservations[0].id)
                .subscribe(() => {
                  this.reservations = this.reservations.filter(reserve => reserve.id !== getExpiredReservations[0].id)
                  console.log('deleted Reservations: ',getExpiredReservations)
                })
              }
              else
              {
                console.log("No Expired Reservation's")
              }
          }
        }
 
      }
    })
  }

  

  deleteOutdatedLoans() {
    this.loanService.getAllLoans().subscribe({
      next: (all_loans) => {
        this.total_loans = all_loans;
        if (this?.total_loans) {
            for (let date of this.total_loans) {
              // get book return date
              var storedDates = date.return_date;
              // console.log('storedDates: ',storedDates)          
    
              // convert return date to type Date
              var storedDates_converted = new Date(storedDates)
                  
              if (this.dateToday > storedDates_converted) {
                var storedDates_reverted = formatDate(storedDates_converted, 'yyyy/MM/dd', 'en-US');
    
                // get all loans that has higher 'return_date' than current day today and then delete them.
                var getExpiredLoans = this.total_loans.filter((loan) => {
                return ((loan["return_date"] == storedDates_reverted))
              })
           
                // Delete All Outdated loan's
                this.loanService.DeleteLoan(getExpiredLoans[0].id)
                .subscribe(() => {
                this.total_loans = this.total_loans.filter(loan => loan.id !== getExpiredLoans[0].id)
                console.log('deleted Loans: ',getExpiredLoans)
              })    
            }
              else
              {
                console.log("No Expired Loan's")
              }
            }  
        }
        
      }
    })
  }


  checkStatus() {
    if (!this.bookBorrowed && !this.bookReserved) {
      this.isDisabled_reserveBtn = true;
    }
  }

  
  checkReservation() {
    this.reserveService.getAllReservations().subscribe({
      next: (all_reservations) => {
        this.reservations = all_reservations;
        this.userId = this.authService.currentUserValue.id;

        if (this.reservations) {
          this.userLoggedIn_res = this.reservations.find((res) => {
            return ((res["bookId"] === this.bookId) && (res["userId"] === this.userId));
         })
  
         this.bookReserved = this.reservations.find((res) => {
           return ((res["bookId"] === this.bookId) && (res["userId"] !== this.userId));    
         })
        }

        if (this.userLoggedIn_res) {
          this.isDisabled_reserveBtn = true; // NOT Active
          console.log('[ YOU ]  have an active reservation')       
        }
        else if (this.bookReserved)
        {             
          this.isDisabled_reserveBtn = true;
          console.log('[ SOMEONE ] have an active reserve')
        }
        else {

          if (!this.bookBorrowed && !this.bookReserved) {
            this.isDisabled_reserveBtn = true;
          }
          else if (!this.bookReserved) {
            this.isDisabled_reserveBtn = false
          }
          else {
            this.isDisabled_reserveBtn = false
          }
          console.log('[ ANY ] can reserve this book')     
        }
      }
    })
  }

  checkLoan() {
    this.loanService.getAllLoans().subscribe({
      next: (all_loans) => {
        this.loans = all_loans;
        this.userId = this.authService.currentUserValue.id;
 
       if (this?.loans) {
        // user: logged in | check if user logged in has borrowed a book  
        this.userLoggedIn = this.loans.find((loan) => {
          return ((loan["bookId"] === this.bookId) && (loan["userId"] === this.userId));
        })

        // user: (NOT) logged in | check if book is borrowed by another
        this.bookBorrowed = this.loans.find((loan) => {
          return ((loan["bookId"] === this.bookId) && (loan["userId"] !== this.userId));    
        })
       }
        
        if (this.userLoggedIn) {
          this.isDisabled_loanBtn = true; // inactive
          console.log('[ YOU ]  have an active loan')       
        }
        else if (this.bookBorrowed)
        {             
          this.isDisabled_loanBtn = true;
          console.log('[ SOMEONE ] have an active loan')
        }
        else {
          this.isDisabled_loanBtn = false; // active
          console.log('[ ANY ] can loan this book')
        }
      },
    });
  }
  

  // (CHECK) if Loan's/Reservation's already exist's
  checkIfLoanOrReservationExists() { 
    this.checkLoan();
    this.checkReservation();
}


  loan(book:Book){
    if (this.authService.currentUserValue == null || this.authService.currentUserValue.id == 0) {
      alert("Do you have any account? If yes, then Login, otherwise create a new account..");
      this.router.navigate(['/Login']);
    }
    else 
    {
      this.bookId = this.book.id;
      this.router.navigate(['/loan',this.bookId]);
    }  
  }

  reserve(book:Book){
    if (this.authService.currentUserValue == null || this.authService.currentUserValue.id == 0) {
      alert("Do you have any account? If yes, then Login, otherwise create a new account..");
      this.router.navigate(['login']);
    }
    else 
    {
      this.bookId = this.book.id;
      this.router.navigate(['/reserve',this.bookId]);
    } 
  }

}
