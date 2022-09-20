import { formatDate } from '@angular/common';
import { Component, ElementRef, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Reservation } from 'app/_models/Reservation';
import { AuthService } from 'app/_services/auth.service';
import { ReservationService } from 'app/_services/reservation.service';
import { CategoryService } from 'app/_services/category.service';
import { BookService } from 'app/_services/book.service';
import { Book } from 'app/_models/Book';
import Swal from 'sweetalert2';
import moment from 'moment';
import { LoanService } from 'app/_services/loan.service';
import { Loan } from 'app/_models/Loan';

@Component({
  selector: 'app-reservation',
  templateUrl: './reservation.component.html',
  styleUrls: ['./reservation.component.css']
})
export class ReservationComponent implements OnInit {
  dateRangeForm!: FormGroup;
  reservations: Reservation[] = [];
  reservation: Reservation = { id: 0, bookId: 0, userId: 0, reserved_At: '', reserved_To: ''}
  bookId: number = 0;
  return_date: string = '';
  reserved_at: string = '';
  currentDate = new Date();
  dateNow = formatDate(this.currentDate, 'yyyy-MM-dd', 'en-US');
  book: Book = { id: 0, title: '', language: '', description: '', publishYear: 0, categoryId: 0, authorId: 0, publisherId: 0, image: '', category: []};
  public formatted_return_date;
  public formatted_reserved_at;
  public minDate = new Date();
  public maxDate = new Date();
  total_loans: Loan[] = [];
  isDisabled_reserveBtn = true;

  constructor(private elementRef: ElementRef, private loanService: LoanService, private router: Router, private reservationService: ReservationService, private categoryService: CategoryService,  private formBuilder: FormBuilder, private bookService: BookService, private route:ActivatedRoute, private authService: AuthService) { }

  range = new FormGroup({
    fromDate: new FormControl('', Validators.required),
    toDate: new FormControl('', Validators.required)
  });

  ngOnInit(): void {
    this.elementRef.nativeElement.ownerDocument.body.style.backgroundColor = '#472c19';
    this.dateRangeForm = this.formBuilder.group({
      fromDate: new FormControl('', Validators.required),
      toDate: new FormControl('', Validators.required),
    });

    // gets clicked reservation.id
    this.bookId = this.route.snapshot.params['id'];
    this.bookService.getBookById(this.bookId).subscribe(x => { this.book = x; })
    this.setReservationMinDate();
  }

  setReservationMaxDate() {
    // convert minimum date: (reserved_at) to Date() format
    var convertStringToDate = new Date(this.reserved_at);

    // Increment minimum date: (reserved_at) with 1 day - Because You Atleast Need To Rent The Book For 1 day 
    this.maxDate.setDate(convertStringToDate.getDate() + 1);
  }

  setReservationMinDate() {
    this.loanService.getAllLoans().subscribe({
      next: (all_loans) => {
        this.total_loans = all_loans;

        // get loan that contains clicked book
        var getBookReturnDate = this.total_loans.filter((loan) => {
          return ((loan["bookId"] == this.bookId))
        })

        // save current loan's return_date
        var returnDate_converted = new Date(getBookReturnDate[0].return_date)

        // set start reservation minimum date
        this.minDate.setDate(returnDate_converted.getDate() + 1);  
      }
    })
  }


  onFormSubmit() {
    if (this.authService.currentUserValue.id !== null && this.authService.currentUserValue.id > 0) {

      this.formatted_return_date = moment(this.return_date).format("YYYY/MM/DD")  
      this.formatted_reserved_at = moment(this.reserved_at).format("YYYY/MM/DD")  

        let reservationitem: Reservation = {
          id: 0,
          userId: this.authService.currentUserValue.id,
          bookId: this.book.id,
          reserved_To: this.formatted_return_date,
          reserved_At: this.formatted_reserved_at
        }

        this.reservation = reservationitem;
        console.log('reservation object: ',this.reservation)
        if (this.reservation) {
          this.reservationService.addReservation(this.reservation)
          .subscribe({
            next: (x) => {
              this.reservations.push(x);
              this.reservation = { id: 0, bookId: 0, userId: 0, reserved_At: '', reserved_To: ''}
              this.reserved_at = '';
              this.return_date = '';
              Swal.fire({
                title: 'Success!',
                text: 'book reserved successfully!',
                icon: 'success',
                confirmButtonText: 'Continue'
              });
              console.log('reservation added successfully!')
              this.router.navigate(['book_details/',this.bookId]);
              
            },
            error: (err) => {
              console.log(err.error);
              // this.message = Object.values(err.error.errors).join(", ");
            }
          });
        }
      }
    }
}
