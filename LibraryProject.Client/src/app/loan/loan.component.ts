import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BookService } from '../_services/book.service';
import { CategoryService } from '../_services/category.service';
import { Observable } from 'rxjs';
import { LoanService } from '../_services/loan.service';
import { Loan } from '../_models/Loan';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { environment } from '../../environments/environment';
import { ActivatedRoute, Router } from '@angular/router';
import { Book } from '../_models/Book';
import { AuthService } from '../_services/auth.service';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {formatDate} from '@angular/common';
import Swal from 'sweetalert2'
import moment from 'moment';


@Component({
  selector: 'app-loan',
  templateUrl: './loan.component.html',

  styleUrls: ['./loan.component.css']
})
export class LoanComponent implements OnInit {
  dateRangeForm!: FormGroup;
  loans: Loan[] = [];
  loan: Loan = { id: 0, bookId: 0, userID: 0, return_date: '', loaned_At: ''}
  bookId: number = 0;
  return_date: string = ''
  loaned_at: string = ''
  currentDate = new Date();
  dateNow = formatDate(this.currentDate, 'yyyy-MM-dd', 'en-US');
  minDate = new Date(this.dateNow);
  minDate2 = new Date(this.minDate)
  public formatted_return_date;
  public formatted_loaned_at;
  total_loans: Loan[] = [];

  book:Book = {id: 0, title: "", description: "", language: "", image: "",publishYear:0, authorId:0, categoryId:0,publisherId:0, author:{id:0,firstName:"",lastName:""} , publisher: {id:0, name:""}};

  constructor(private loanService: LoanService, private router: Router, private bookService: BookService, private categoryService: CategoryService,  private formBuilder: FormBuilder, private loanservice: LoanService, private route:ActivatedRoute, private authService: AuthService) {}

  range = new FormGroup({
    fromDate: new FormControl('', Validators.required),
    toDate: new FormControl('', Validators.required)
  });

  ngOnInit(): void {
    this.dateRangeForm = this.formBuilder.group({
      fromDate: new FormControl('', Validators.required),
      toDate: new FormControl('', Validators.required),
    });

    // gets clicked book.id
    this.bookId = this.route.snapshot.params['id'];

    this.bookService.getBookById(this.bookId).subscribe(x => {
      this.book = x;
    })

    this.loanservice.getAllLoans().subscribe((loan) => {
      this.loans = loan || [];
    })

    this.setLoanMinDate();
  }

  setLoanMinDate() {
    this.return_date = this.loaned_at + 1;
    console.log('return_date: ',this.return_date)
  }


  onFormSubmit() {
    console.log('Is Form Invalid', this.dateRangeForm.invalid);

    if (this.authService.currentUserValue.id != null && this.authService.currentUserValue.id > 0) {     

        this.formatted_return_date = moment(this.return_date).format("YYYY/MM/DD")  
        this.formatted_loaned_at = moment(this.loaned_at).format("YYYY/MM/DD")  

        let loanitem: Loan = {
          id: this.book.id,
          userID: this.authService.currentUserValue.id,
          bookId: this.book.id,
          return_date: this.formatted_return_date,
          loaned_At: this.formatted_loaned_at
        }
          
          this.loan = loanitem;
          // console.log('loanitem: ',this.loan)
          if (loanitem.id > 0) {
            console.log('loan: ',this.loan)
            this.loanservice.addLoan(this.loan)
            .subscribe({
            next: (x) => {
              this.loans.push(x);
              this.loan = { id: 0, bookId: 0, userID: 0, return_date: '', loaned_At: ''}
              this.loaned_at = '';
              this.return_date = '';
              Swal.fire({
                title: 'Success!',
                text: 'book burrow successfully!',
                icon: 'success',
                confirmButtonText: 'Continue'
              });
              console.log('loan added successfully!')
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
  