import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
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
  return_date: string = ''
  reserved_at: string = ''
  currentDate = new Date();
  dateNow = formatDate(this.currentDate, 'yyyy-MM-dd', 'en-US');
  // minDate = new Date(this.dateNow);
  // minDate2 = new Date(this.minDate)
  book: Book = {id: 0, title: "", description: "", language: "", image: "",publishYear:0, authorId:0, categoryId:0,publisherId:0, author:{id:0,firstName:"",lastName:""} , publisher: {id:0, name:""}};
  public formatted_return_date;
  public formatted_reserved_at;
  public minDate = new Date();
  total_loans: Loan[] = [];

  constructor(private loanService: LoanService, private router: Router, private reservationService: ReservationService, private categoryService: CategoryService,  private formBuilder: FormBuilder, private bookService: BookService, private route:ActivatedRoute, private authService: AuthService) { }

  range = new FormGroup({
    fromDate: new FormControl('', Validators.required),
    toDate: new FormControl('', Validators.required)
  });

  ngOnInit(): void {
    this.dateRangeForm = this.formBuilder.group({
      fromDate: new FormControl('', Validators.required),
      toDate: new FormControl('', Validators.required),
    });

    // gets clicked reservation.id
    this.bookId = this.route.snapshot.params['id'];

    this.bookService.getBookById(this.bookId).subscribe(x => {
      this.book = x;
    })
    this.setReservationMinDate();
  }

  setReservationMinDate() {
    this.loanService.getAllLoans().subscribe({
      next: (all_loans) => {
        this.total_loans = all_loans;

        // get loan that contains clicked book
        var getBookReturnDate = this.total_loans.filter((loan) => {
          return ((loan["bookId"] == this.bookId))
        })
        var returnDate_converted = new Date(getBookReturnDate[0].return_date)
        this.minDate = returnDate_converted;
      }
    })
  }


  onFormSubmit() {
    console.log('Is Form Invalid', this.dateRangeForm.invalid);
    console.log('book id: ',this.book.id)

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

        // console.log('reservationitem: ',reservationitem)
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
              this.router.navigate(['Book-Details/',this.bookId]);
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
