import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BookService } from '../_services/book.service';
import { CategoryService } from '../_services/category.service';
import { DarkModeService } from 'angular-dark-mode';
import { Observable } from 'rxjs';
import { LoanService } from '../_services/loan.service';
import { Loan } from '../_models/Loan';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { environment } from '../../environments/environment';
import { ActivatedRoute } from '@angular/router';
import { Book } from '../_models/Book';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-loan',
  templateUrl: './loan.component.html',

  styleUrls: ['./loan.component.css']
})
export class LoanComponent implements OnInit {
  dateRangeForm!: FormGroup;
  loans: Loan[] = [];
  bookId: number = 0;

  book:Book={id: 0, title: "", description: "", language: "", image: "",publishYear:0, authorId:0, categoryId:0,publisherId:0, author:{id:0,firstName:"",lastName:""} , publisher: {id:0, name:""}};

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

    this.route.params.subscribe(params => {
      this.bookId = +params['id'];
    });
    this.bookService.getBookById(this.bookId).subscribe(x=>
      { this.book=x,

      console.log('bookId: ',this.book.id);
    });

  }
  onFormSubmit() {
    console.log('Is Form Invalid', this.dateRangeForm.invalid);

    if (this.authService.currentUserValue.id != null && this.authService.currentUserValue.id > 0) {



      // let loanitem: Loan = {

      //   userId: this.authService.currentUserValue.id,
      //   bookId: this.book.id,
      //   return_date: ,

      //   loaned_At:
      // }

      }

  }


}
