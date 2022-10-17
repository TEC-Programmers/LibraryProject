import { Component, ElementRef, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { BookService } from '../_services/book.service';
import { CategoryService } from '../_services/category.service';
import { LoanService } from '../_services/loan.service';
import { Loan } from '../_models/Loan';
import { ActivatedRoute, Router } from '@angular/router';
import { Book } from '../_models/Book';
import { AuthService } from '../_services/auth.service';
import {formatDate} from '@angular/common';
import Swal from 'sweetalert2'
import moment from 'moment';

@Component({
  selector: 'app-loan',
  templateUrl: './loan.component.html',
  styleUrls: ['./loan.component.css']
})

export class LoanComponent implements OnInit {
  dateRangeForm!: UntypedFormGroup;
  loans: Loan[] = [];
  loan: Loan = { id: 0, bookId: 0, userId: 0, return_date: '', loaned_At: ''}
  bookId: number = 0;
  return_date: string = '';
  loaned_at: string = '';
  currentDate = new Date();
  dateNow = formatDate(this.currentDate, 'yyyy-MM-dd', 'en-US');
  minDate = new Date(this.dateNow);
  maxDate = new Date();
  public formatted_return_date;
  public formatted_loaned_at;
  total_loans: Loan[] = [];
  book: Book = { id: 0, title: '', language: '', description: '', publishYear: 0, categoryId: 0, authorId: 0, publisherId: 0, image: '', category: []};
  message: string = '';

  constructor(private elementRef: ElementRef, private loanService: LoanService, private router: Router, private bookService: BookService, private categoryService: CategoryService,  private formBuilder: UntypedFormBuilder, private loanservice: LoanService, private route:ActivatedRoute, private authService: AuthService) {}

  ngOnInit(): void {
    this.elementRef.nativeElement.ownerDocument.body.style.backgroundColor = '#472c19';
    this.dateRangeForm = this.formBuilder.group({
      fromDate: new UntypedFormControl('', Validators.required),
      toDate: new UntypedFormControl('', Validators.required),
    });

    // gets id of clicked book
    this.bookId = this.route.snapshot.params['id'];

    this.bookService.getBookById(this.bookId).subscribe(x => {
      this.book = x;
    })

    this.loanservice.getAllLoans().subscribe((loan) => {
      this.loans = loan || [];
    })
  }


  setLoanMinDate() {
    // convert minimum date: (loaned_at) to Date() format
    var convertStringToDate = new Date(this.loaned_at);

    // Increment minimum date: (loaned_at) with 1 day - Because You Atleast Need To Have The Book For 1 day 
    this.maxDate.setDate(convertStringToDate.getDate() + 1);
  }


  onFormSubmit() {
    console.log('Is Form Invalid', this.dateRangeForm.invalid);
    if (this.authService.currentUserValue.id != null && this.authService.currentUserValue.id > 0) {     

        this.formatted_return_date = moment(this.return_date).format("YYYY/MM/DD")  
        this.formatted_loaned_at = moment(this.loaned_at).format("YYYY/MM/DD")  

        let loanitem: Loan = {
          id: this.book.id,
          userId: this.authService.currentUserValue.id,
          bookId: this.book.id,
          return_date: this.formatted_return_date,
          loaned_At: this.formatted_loaned_at
        }
          
          this.loan = loanitem;
          if (loanitem.id > 0) {
            console.log('Your Loan: ',this.loan)
            this.loanservice.addLoan(this.loan)
            .subscribe({
            next: (x) => {
              this.loans.push(x);
              this.loan = { id: 0, bookId: 0, userId: 0, return_date: '', loaned_At: ''}
              this.loaned_at = '';
              this.return_date = '';
              Swal.fire({
                title: 'Success!',
                text: 'Book Borrowed Successfully!',
                icon: 'success',
                confirmButtonText: 'Continue'
              });
              this.router.navigate(['book_details/',this.bookId]);
            },
            error: (err) => {
              console.log(err.error);
              this.message = Object.values(err.error.errors).join(", ");
            }
          });
        }
      }
    }
       
}
  