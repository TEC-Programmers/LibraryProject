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
  IdFound!: any;
  total_loans: Loan[] = [];
  loans: Loan[] = [];
  reservations: Reservation[] = [];
  loanObjFound: Loan | undefined = { id: 0, userID: 0, bookId: 0, loaned_At: '', return_date: '' }
  userID: number = 0;
  books: Book[] = [];
  users: User[] = []

  
 
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

    // this.findBookIdInLoanTable();
    this.getLoan_();    
  }

  

  

  // (CHECK) if Loan already exists
  getLoan_() {
    this.loanService.getAllLoans().subscribe({
      next: (all_loans) => {
        this.loans = all_loans;
        this.userID = this.authService.currentUserValue.id;

        console.log('search userID: ',this.userID)
        console.log('search bookId: ',this.bookId)

        for (var i = this.loans.length -1; i > -1; i--) {
          let userid = this.loans[i].userID != this.userID;

          // bruger er logget ind og har lånt bog
          if (this.loans[i].bookId === this.bookId && this.loans[i].userID === this.userID) {
            console.log('Loan Found')
            this.isDisabled_loanBtn = true;
            this.isDisabled_reserveBtn = true;
          }
          else if (this.loans[i].bookId === this.bookId)
          {
              // Book is in burrowed
              console.log('Book ready for reservation!')
              this.isDisabled_loanBtn = true;
              this.isDisabled_reserveBtn = false;
          }
          else if (this.loans[i].bookId != this.bookId)
          {
            // Book is (NOT) in [Loan] table and is availabe for users to loan and reserve
            this.isDisabled_loanBtn = false;
            this.isDisabled_reserveBtn = true;
          }

          // Object.keys(loan).forEach(prop => {
          //   console.log(prop)
          //   console.log(loan[prop]);

          //   if (loan[prop].bookId === 3) {
          //     console.log('found')
          //   }
          //   else
          //   {
          //     console.log('NOT found')
          //   }
          // });           
      
        }


        // let filterResult: any = this.loans.filter(loan =>
        //   loan.bookId == this.bookId && loan.userID == this.userID);
        //   console.log('new obj: ',JSON.stringify(filterResult))



        // var result = this.loans.filter((o, i) => {
        //   return ((o["bookId"] === this.bookId) && o.userID === this.userID);
        // })
        // console.log('object: ',result)

        // const returnLoanWithBookId = this.loans.filter((obj) => {
        //     return obj.bookId === this.bookId && obj.userID === this.userID;
        //   });

        //   console.log(returnLoanWithBookId)




        // const first = this.loans.find((obj) => {
        //   return obj.bookId === this.bookId;
        // });
        // console.log('first: ',first)
        // if (first) {
        //   console.log('anyone')
        // }
        // else
        // {
        //   console.log('first is false')
        // }
      },
    });    
}

  findBookIdInLoanTable(): void {
    this.loanService.getAllLoans().subscribe({
      next: (all_loans) => {
        this.loans = all_loans;
        this.userID = this.authService.currentUserValue.id;
        console.log('userID: ',this.userID)
        console.log('bookId: ',this.bookId)

        for (const key in this.loans) {
          if (this.loans.hasOwnProperty(key)) {
            // console.log(`key: ${key} : object: ${this.loans[key]}`)

            // check if user loggin in has burrow book
            if (this.loans[key].bookId == this.bookId && this.loans[key].userID == this.userID) {
              //this.isDisabled_loanBtn = true; // NOT Active
              //this.isDisabled_reserveBtn = true;
              console.log('me')
            }
            else {
              console.log('not')
            }
            // else if (this.loans[key].bookId === this.bookId && (this.loans[key].userID != this.userID)) {
            //   // find anyone who has burrow book
            //   this.isDisabled_loanBtn = true;
            //   this.isDisabled_reserveBtn = false;
            //   console.log('anyone')
            // }
            // else {
            //   this.isDisabled_loanBtn = false;
            //   this.isDisabled_reserveBtn = true;
            //   console.log('book available')
            // }
          }
        }
      
        

        

        // check if user loggin in has burrow book
        // const findUserLoggin = this.loans.filter(el => {
        //   return el.bookId === this.bookId && el.userID === this.userID;
        // })

        // find anyone who has burrow book
        // const findUser = this.loans.filter(el => {
        //   return el.bookId === this.bookId;
        // })

        
    //     if (findUserLoggin) { 
    //       console.log('My loan found: ',findUserLoggin)
    //       this.isDisabled_loanBtn = true; // NOT Active
    //       this.isDisabled_reserveBtn = true;
    //     } 
    //     else if (findUser)
    //     {
    //       console.log('loan found and current user have not burrow this book')
    //       this.isDisabled_loanBtn = true;
    //       this.isDisabled_reserveBtn = false;
    //       this.findBookInReserve();
    //     }
    //     else // bog (IKKE) lånt
    //     {
    //       this.isDisabled_loanBtn = false;
    //       this.isDisabled_reserveBtn = true;
    //       console.log('book available')
    //     }
    //   },
    //   error: (err: any) => {
    //     console.log(err);
    //   },
    //   complete: () => {
    //     console.log('getAllCustomers() - Completed Successfully');
      },
    });

  }

  // this.authService.currentUserValue.firstName; 
  findBookInReserve(): void {
    this.reserveService.getAllReservations().subscribe({
      next: (all_res) => {
        this.reservations = all_res;
        this.userID = this.authService.currentUserValue.id;

        // check if user loggin in has reserved book
        const findUserLoggin = this.reservations.filter(el => {
          return el.bookId === this.bookId && el.userId === this.userID;
        })

        // find anyone who has reserved book
        const findUser = this.reservations.filter(el => {
          return el.bookId === this.bookId;
        })

        if (findUserLoggin) {    //&& res.userID === 
          console.log('res found')
          this.isDisabled_reserveBtn = true;
          this.isDisabled_loanBtn = true;
        } 
        else if (findUser)
        {
          this.isDisabled_reserveBtn = true;
          this.isDisabled_loanBtn = true;
        }    
        else 
        {
          console.log('res not found')
          this.isDisabled_reserveBtn = false;
          this.isDisabled_loanBtn = true;
        } 
      },
      error: (err: any) => {
        console.log(err);
      },
      complete: () => {
        console.log('findBookInReserve() - Completed Successfully');
      },
    });
  }

  loan(book:Book){
    if (this.authService.currentUserValue == null || this.authService.currentUserValue.id == 0) {
      alert("Do you have any account? If yes, then Login, otherwise create a new account..");
      this.router.navigate(['login']);
    }
    else
      this.bookId = this.book.id;
      // if ()
      // this.isDisabled_reserveBtn = true;
      console.log('book-details: bookId: ',this.bookId)
      this.router.navigate(['/loan',this.bookId]);
  }


  reserve(book:Book){


  }


}
