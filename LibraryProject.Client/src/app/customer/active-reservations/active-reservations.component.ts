import { formatDate } from '@angular/common';
import { Component, ElementRef, OnInit } from '@angular/core';
import { Book } from 'app/_models/Book';
import { Reservation } from 'app/_models/Reservation';
import { AuthService } from 'app/_services/auth.service';
import { BookService } from 'app/_services/book.service';
import { ReservationService } from 'app/_services/reservation.service';

@Component({
  selector: 'app-active-reservations',
  templateUrl: './active-reservations.component.html',
  styleUrls: ['./active-reservations.component.css']
})
export class ActiveReservationsComponent implements OnInit {
  total_Reservations: Reservation[] = [];
  getAll_Reservations: Reservation[] = [];

  yourReservations: Reservation[] = [];
  yourReservation: Reservation | undefined = { id: 0, bookId: 0, userId: 0, reserved_At: '', reserved_To: ''}

  total_books: Book[] = [];
  yourBooks: Book[] = [];
  yourBook: Book | undefined = { id: 0, title: '', language: '', description: '', publishYear: 0, categoryId: 0, authorId: 0, publisherId: 0, image: '', category: []};
  getAll_books: Book[] = [];

  yourId: number = 0;
  reservationId: number = 0;
  searchText: string = '';
  p:any;
  public bookId: number = 0;
  public value;
  public reservationArray: Array<Reservation> = [];
  getBooksInReservation: number = 0;
  public bookArray: Array<Book> = [];
  reservations: Reservation[] = [];
  dateToday = new Date();
  color1;
  color2;

  constructor(private reservationService: ReservationService, private authService: AuthService, private bookService: BookService, private elementRef: ElementRef) { }

  ngOnInit(): void {
    // this.getActiveReservations();
    this.deleteOutdatedReservations();
  }

  ngAfterViewInit(){
    this.elementRef.nativeElement.ownerDocument.body.style.backgroundColor = '#607D8B';
    // this.elementRef.nativeElement, "style", ``
 }

  deleteOutdatedReservations() {
    this.reservationService.getAllReservations().subscribe({
      next: (all_reservations) => {
        this.total_Reservations = all_reservations;

        if (this?.total_Reservations) {
            for (let date of this.total_Reservations) {
              // get book return date
              var storedDates = date.reserved_To;

              // convert return date to type Date
              var storedDates_converted = new Date(storedDates)
    
              if (this.dateToday > storedDates_converted) {
                var storedDates_reverted = formatDate(storedDates_converted, 'yyyy/MM/dd', 'en-US');
    
                // get all loans that has higher 'return_date' than current day today and then delete them.
                var getExpiredReservations = this.total_Reservations.filter((reservation) => {
                return ((reservation["reserved_To"] == storedDates_reverted))
              })

              if (getExpiredReservations) {
                // Delete All Outdated reservation's
                this.reservationService.deleteReservation(getExpiredReservations[0].id)
                .subscribe(() => {
                  this.reservations = this.reservations.filter(reserve => reserve.id !== getExpiredReservations[0].id)
                  console.log('deleted Reservations: ',getExpiredReservations)
                  this.getActiveReservations();
                })
              } 
              else {
                this.getActiveReservations();
              }               
            }
              else
              {
                this.getActiveReservations();
                console.log("No Expired Reservation's")
              }
          }
        }
 
      }
    })
  }


  getActiveBookInRes(bookId: number) {
    this.bookService.getAllBooks().subscribe({
      next: (all_books) => {
        this.total_books = all_books;           
        
        // get book associated with current loan
        this.yourBook = this.total_books.find((book) => {
          return book.id === bookId;
        })

        if (this.yourBook) {
          this.bookArray.push(this.yourBook);
        }
        else {
          console.log('res getActiveBookInLoan() Error: Book (NOT) Found!')
        }
      }          
    })
  }


  getActiveReservations() {
    this.reservationService.getAllReservations().subscribe({
      next: (all_Reservations) => {
        this.total_Reservations = all_Reservations;
        console.log('total_Reservations: ',this.total_Reservations)
        this.yourId = this.authService.currentUserValue.id;

        if (this.total_Reservations) {
          // get all Reservations that current user have made and save it to variable: [ this.yourReservations ] 
          this.yourReservations = this.total_Reservations.filter((reservation) => {
          return reservation.userId === this.yourId;
        });

        
        if (this.yourReservations) {
          // loop through [ this.yourReservations ] to get specific book in your Reservation
          this.yourReservations.forEach(reservation => {
            var getLength = this.yourReservations.length;
            this.bookId = reservation.bookId;
            this.reservationArray.push(reservation)
            if (this.reservationArray.length <= getLength) {
              this.getActiveBookInRes(this.bookId);
            }
          });
        }
        else {
          this.yourReservations = [];
        }
      }
        console.log('yourReservations: ',this.yourReservations)
        
      },
      error: (err: any) => {
        console.log('getActiveReservations() Error: ',err)
      },
      complete: () => {
        console.log('getActiveReservations() - Completed Successfully');
      }
    })
  }
}
