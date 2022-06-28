import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Book } from '../_models/Book';
import { BookService } from '../_services/book.service';
import { AuthService } from '../_services/auth.service';
import { LoanService } from 'app/_services/loan.service';
import { Reservation } from 'app/_models/Reservation';
import { ReservationService } from 'app/_services/reservation.service';
import { dateInputsHaveChanged } from '@angular/material/datepicker/datepicker-input-base';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.css']
})
export class BookDetailsComponent implements OnInit {
   bookId:number  = 0;
   userId:number = 0;

  book:Book = { id: 0, title: "", description: "", language: "", image: "",publishYear:0, authorId:0, categoryId:0,publisherId:0, author:{id:0,firstName:"",lastName:""} , publisher: {id:0, name:""}};

  reservations: Reservation[] = []
  reservation: Reservation = {
  id: 0,
  userId: 0,
  user: { id: 0, firstName: "", lastName: "", email: "", password: "", role: 0 },
  bookId: 0,
  book: {
    id: 0, title: "", language: "", image: "", description: "", publishYear: 0, categoryId: 0, authorId: 0,
    author: { id: 0, firstName: "", lastName: "" }, publisherId: 0,
    publisher: { id: 0, name:"" }},
  reserved_At: '',
  reserved_To: '',


}
  isDisabled_loanBtn: boolean= false;


  constructor(private bookService:BookService, private loanService: LoanService, private reservationService:ReservationService, private route:ActivatedRoute, private router: Router, private authService: AuthService, private datepipe:DatePipe ) { }

  ngOnInit(): void {

    this.route.params.subscribe(params => {
      this.bookId = +params['id'];
    });
    this.bookService.getBookById(this.bookId).subscribe(x=>
      { this.book=x,

      console.log('book-details on load: ',this.book);
    });
    // this.DisableIfBookReserve();
    this.reserve();
  }

    DisableIfBookReserve() {
        // this.reservationService.getAllReservations().subscribe(reservation => {
        // this.reservations = reservation;



        //  const bookId = this.reservationService.getReservationById(this.bookId)
        //  this.userId = this.authService.currentUserValue.id

        // console.log("resBookId", this.reservation.bookId, "\n", "bookId:", this.bookId, "\n", "userId:", this.userId);
        // console.log("reservation bookId", this.reservation.bookId)

        // if (this.reservation.userId === this.userId) {
        //   console.log("userid:", this.userId)
        // }

        // else{
        //   console.log("this is not working")
        // }
      //   if (this.reservation.userId === this.userId) {
      //     console.log("found user");

      //     if (this.reservation.bookId === this.bookId) {
      //       console.log("found book");

      //       if (this.reservation.userId === this.userId && this.reservation.bookId === this.bookId) {

      //         console.log("did not find user with book and vice versa");
      //       }

      //       else{

      //         console.log("not reserved by user 3")
      //       }
      //     }

      //     else{
      //  console.log("not reserved by this user 2")
      //     }

        // }


        // else{
        //   console.log("book not reserved by this user 1")
        // }

      //  })
    }


  Loan(book:Book){
    if (this.authService.currentUserValue == null || this.authService.currentUserValue.id == 0) {
      alert("Do you have any account? If yes, then Login, otherwise create a new account..");
      this.router.navigate(['login']);
    }
    else
    this.bookId = this.book.id;
    console.log('book-details: bookId: ',this.bookId)
    this.router.navigate(['/loan',this.bookId]);
  }


  reserve(){
    this.reservationService.getReservationById(this.bookId).subscribe(x=>
      { this.reservation = x,
      console.log("reservation",this.reservations);

      })

      let reserveEnd = this.reservation.bookId;

  }

  // functionGetNewDate(endDate){
  //   const year = getYear(endDate)
  //   const month = getMonth(endDate)
  //   const day = getDay(endDate)
  //   return newEndDate = new Date(year,month,day)
  // }
}

