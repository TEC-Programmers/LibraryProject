import { Component, OnInit } from '@angular/core';
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
  loans: Loan[] | undefined = [];
  reservations: Reservation[] = [];
  loanObjFound: Loan = { id: 0, userID: 0, bookId: 0, loaned_At: '', return_date: '' }
  userID: number = 0;
  books: Book[] = [];
  users: User[] = []
  userLoggedIn_status: boolean = false;
  bookBorrowed_status: boolean = false;
  bookAvailable_status: boolean = false;

  constructor(private userService: UserService, private reserveService: ReservationService, private bookService:BookService, private route:ActivatedRoute, private router: Router, private authService: AuthService, private loanService: LoanService ) { }

  ngOnInit(): void {
    this.bookService.getAllBooks().subscribe(b => this.books = b);
    this.userService.getAllUsers().subscribe(u => this.users = u)

    this.route.params.subscribe(params => {
      this.bookId = +params['id'];
    });

    this.bookService.getBookById(this.bookId).subscribe(x => { 
      this.book = x,
      console.log('book-details on load: ',this.book);
    });

    this.check_Reservation();
    this.check_Loan();    
  }

  
  check_Reservation() {
    this.reserveService.getAllReservations().subscribe({
      next: (all_reservations) => {
        this.reservations = all_reservations;
        this.userID = this.authService.currentUserValue.id;
        console.log('check_Reservation: userID: ',this.userID)
        console.log('check_Reservation: bookId: ',this.bookId)

        var userLoggedIn = this.reservations.find((res) => {
          return ((res["bookId"] === this.bookId) && (res["userId"] === this.userID));
       })

      //  var bookBorrowed = this.reservations.find((res) => {
      //    return ((res["bookId"] === this.bookId) && (res["userId"] !== this.userID));    
      //  })

      //  var bookAvailable = this.reservations.find((res) => {
      //    return ((res["bookId"] == 0));
      //  })


       if (userLoggedIn) {
        this.isDisabled_loanBtn = true; // NOT Active
        this.isDisabled_reserveBtn = true;
        console.log('userLoggedIn')             
      }
      // else
      // {     
      //   this.isDisabled_loanBtn = true;
      //   this.isDisabled_reserveBtn = false;
      //   console.log('bookBorrowed')
      // }
    
      // if (bookAvailable) {
      //   this.isDisabled_loanBtn = false;
      //   this.isDisabled_reserveBtn = true;
      //   console.log('bookAvailable')
      // }

      }
    })
  }
  

  // (CHECK) if Loan already exists
  check_Loan() {
    this.loanService.getAllLoans().subscribe({
      next: (all_loans) => {
        this.loans = all_loans;
        this.userID = this.authService.currentUserValue.id;
        console.log('check_Loan: userID: ',this.userID)
        console.log('check_Loan: bookId: ',this.bookId)

        var userLoggedIn = this.loans.find((loan) => {
           return ((loan["bookId"] === this.bookId) && (loan["userId"] === this.userID));
        })

        var bookBorrowed = this.loans.find((loan) => {
          return ((loan["bookId"] === this.bookId) && (loan["userId"] !== this.userID));    
        })

        var bookAvailable = this.loans.find((loan) => {
          return ((loan["bookId"] == 0));
        })
        
        if (userLoggedIn) {
          this.isDisabled_loanBtn = true; // NOT Active
          this.isDisabled_reserveBtn = true;
          console.log('userLoggedIn')       
        }
        else
        {
               
          this.isDisabled_loanBtn = true;
          this.isDisabled_reserveBtn = false;
          console.log('bookBorrowed')
        }
      
        if (bookAvailable) {
          this.isDisabled_loanBtn = false;
          this.isDisabled_reserveBtn = true;
          console.log('bookAvailable')
        }
      },
    });    
}

  loan(book:Book){
    if (this.authService.currentUserValue == null || this.authService.currentUserValue.id == 0) {
      alert("Do you have any account? If yes, then Login, otherwise create a new account..");
      this.router.navigate(['login']);
    }
    else 
    {
      this.bookId = this.book.id;
      console.log('book-details: bookId: ',this.bookId)
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
      console.log('book-details: bookId: ',this.bookId)
      this.router.navigate(['/reserve',this.bookId]);
    } 
  }


}
