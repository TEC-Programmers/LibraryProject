import { formatDate } from '@angular/common';
import { Component, ElementRef, OnInit } from '@angular/core';
import { Book } from 'app/_models/Book';
import { Loan } from 'app/_models/Loan';
import { User } from 'app/_models/User';
import { AuthService } from 'app/_services/auth.service';
import { BookService } from 'app/_services/book.service';
import { LoanService } from 'app/_services/loan.service';

@Component({
  selector: 'app-active-loans',
  templateUrl: './active-loans.component.html',
  styleUrls: ['./active-loans.component.css']
})
export class ActiveLoansComponent implements OnInit {
  total_loans: Loan[] = [];
  getAll_loans: Loan[] = [];

  yourLoans: Loan[] = [];
  yourLoan: Loan | undefined = { id: 0, bookId: 0, userId: 0, return_date: '', loaned_At: ''}

  total_books: Book[] = [];
  yourBooks: Book[] = [];
  yourBook: Book | undefined = { id: 0, title: '', language: '', description: '', publishYear: 0, categoryId: 0, authorId: 0, publisherId: 0, image: '', category: [], publisher: { id: 0, name: ''}, author: { id: 0, firstName: '', lastName: ''} };
  getAll_books: Book[] = [];

  yourId: number = 0;
  loanId: number = 0;
  searchText: string = '';
  p:any;
  public bookId: number = 0;
  public value;
  public array: Array<Loan> = [];
  getBooksInLoan: number = 0;
  public bookArray: Array<Book> = [];
  dateToday = new Date();

  constructor(private loanService: LoanService, private authService: AuthService, private bookService: BookService, private elementRef: ElementRef) { }

  ngOnInit(): void {
    this.deleteOutdatedLoans();
  }

  ngAfterViewInit(){
    this.elementRef.nativeElement.ownerDocument.body.style.backgroundColor = '#607D8B';
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

              if (getExpiredLoans) {
                  // Delete All Outdated loan's
                  this.loanService.DeleteLoan(getExpiredLoans[0].id)
                  .subscribe(() => {
                  this.total_loans = this.total_loans.filter(loan => loan.id !== getExpiredLoans[0].id)
                  console.log('deleted Loans: ',getExpiredLoans)
                  this.getActiveLoans();
                })    
              } 
              else {
                this.getActiveLoans();
              }              
            }
              else
              {
                this.getActiveLoans();
                console.log("No Expired Loan's")
              }
            }  
        }
        
      }
    })
  }

  getActiveBookInLoan(bookId: number) {
    this.bookService.getAllBooks().subscribe({
      next: (all_books) => {
        this.total_books = all_books;           
        
        // get book associated with current loan
        this.yourBook = this.total_books.find((book) => {
          return book.id === bookId;
        })

        if (this.yourBook) {
          this.bookArray.push(this.yourBook);
          // console.log('Book Found: ', this.yourBooks)
        }
        else {
          console.log('getActiveBookInLoan() Error: Book (NOT) Found!')
        }
      }          
    })
  }

  getActiveLoans() {
    this.loanService.getAllLoans().subscribe({
      next: (all_loans) => {
        this.total_loans = all_loans;
        console.log('total_loans: ',this.total_loans)
        this.yourId = this.authService.currentUserValue.id;

        if (this.total_loans) {
            // get all loans that current user have made and save it to: [ this.yourLoans ] 
            this.yourLoans = this.total_loans.filter((loan) => {
              return loan.userId === this.yourId;
          });
        }

        // loop through [ this.yourLoans ] to get specific book in your loan
        this.yourLoans.forEach(loan => {
          this.bookId = loan.bookId;
          this.array.push(loan)
          if (this.array.length > 0) {
            this.getActiveBookInLoan(this.bookId);
          }
        });
      },
      error: (err: any) => {
        console.log('getActiveLoans() Error: ',err)
      },
      complete: () => {
        console.log('getActiveLoans() - Completed Successfully');
      }
    })
  }

}
