import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BookService } from '../_services/book.service';
import { CategoryService } from '../_services/category.service';
import { Observable } from 'rxjs';
import { LoanService } from '../_services/loan.service';
import { Loan } from '../_models/Loan';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { environment } from '../../environments/environment';
import { ActivatedRoute } from '@angular/router';
import { Book } from '../_models/Book';
import { AuthService } from '../_services/auth.service';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {formatDate} from '@angular/common';
import Swal from 'sweetalert2'


@Component({
  selector: 'app-loan',
  templateUrl: './loan.component.html',

  styleUrls: ['./loan.component.css']
})
export class LoanComponent implements OnInit {
  dateRangeForm!: FormGroup;
  loans: Loan[] = [];
  loan: Loan = { id: 0, bookId: 0, userId: 0, return_date: '', loaned_At: ''}
  bookId: number = 0;
  return_date: string = ''
  loaned_at: string = ''
  currentDate = new Date();
  dateNow = formatDate(this.currentDate, 'yyyy-MM-dd', 'en-EU');
  minDate = new Date(this.dateNow);
  minDate2 = new Date(this.minDate)


  book:Book = {id: 0, title: "", description: "", language: "", image: "",publishYear:0, authorId:0, categoryId:0,publisherId:0, author:{id:0,firstName:"",lastName:""} , publisher: {id:0, name:""}};

  constructor(private bookService: BookService, private categoryService: CategoryService,  private formBuilder: FormBuilder, private loanservice: LoanService, private route:ActivatedRoute, private authService: AuthService) {}

  range = new FormGroup({
    fromDate: new FormControl('', Validators.required),
    toDate: new FormControl('', Validators.required)
  });

  ngOnInit(): void {
    this.dateRangeForm = this.formBuilder.group({
      fromDate: new FormControl('', Validators.required),
      toDate: new FormControl('', Validators.required),
    });


    // gets clicked book id
    this.bookId = this.route.snapshot.params['id'];
    console.log('bookId: ',this.bookId)

    this.bookService.getBookById(this.bookId).subscribe(x => {
      this.book = x
      console.log('book: ',this.book);
    })
  }
  onFormSubmit() {
    console.log('Is Form Invalid', this.dateRangeForm.invalid);

    if (this.authService.currentUserValue.id != null && this.authService.currentUserValue.id > 0) {
        let loanitem: Loan = {
          id: this.book.id,
          userId: this.authService.currentUserValue.id,
          bookId: this.book.id,
          return_date: this.return_date,
          loaned_At: this.loaned_at
        }
        console.log('loanitem: ',loanitem)
        this.loan = loanitem;
        console.log('loan: ',this.loan)
        if (this.loan) {
          this.loanservice.addLoan(this.loan)
          .subscribe({
            next: (x) => {
              this.loans.push(x);
              this.loan = { id: 0, bookId: 0, userId: 0, return_date: '', loaned_At: ''}
              this.loaned_at = '';
              this.return_date = '';
              Swal.fire({
                title: 'Success!',
                text: 'loan added successfully',
                icon: 'success',
                confirmButtonText: 'Continue'
              });
              console.log('loan added successfully!')
            },
            error: (err) => {
              console.log(err.error);
              // this.message = Object.values(err.error.errors).join(", ");
            }
          });
        }
      }
    }

    // resetForm(): void {
    //   if (this.loaned_at && this.return_date) {

    //   }
    //   else {
    //     console.log('data NOT valid.')
    //   }

    // }




  }
