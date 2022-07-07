import { Component, OnInit, ChangeDetectorRef, AfterContentChecked } from '@angular/core';
import { environment } from '../../environments/environment';
import { Author } from '../_models/Author';
import { Book } from '../_models/Book';
import { Loan } from '../_models/Loan';
import { AuthorService } from '../_services/author.service';
import { LoanService } from '../_services/loan.service';
import { BookService } from '../_services/book.service';
import { AuthService } from 'app/_services/auth.service';
import Swal from 'sweetalert2';
import { User } from 'app/_models/User';
import { first, Observable, of, subscribeOn, Subscriber } from 'rxjs';
import { UserService } from 'app/_services/user.service';


@Component({
  selector: 'app-frontpage',
  templateUrl: './frontpage.component.html',
  styleUrls: ['./frontpage.component.css']
})
export class FrontpageComponent implements OnInit {
  currentUser: User = { id: 0, firstName: '', middleName: '', lastName: '', email: '', password: '', role: 0 };
  allBooks: Book[] = [];
  books : Book[]= [];
  disabled: boolean = true;
  public x: number = 0;
  z: any;

  constructor(private bookService: BookService, private authService: AuthService, private userService: UserService) { }

  ngOnInit(): void {
    this.authService.currentUser.subscribe(x => this.currentUser = x);

    if (this.authService.currentUserValue != null && this.authService.currentUserValue.id > 0) {
      console.log('current Role: ',this.authService.currentUserValue.role)
    }
  }

  ngAfterContentChecked(): void {
    this.showOrhideAdminBtn();  
  }

  showOrhideAdminBtn() {
    this.authService.currentUser.subscribe(x => {
    this.currentUser = x;
  
    if (this.currentUser) {
      this.userService.getRole$.subscribe(x => this.x = x); // start listening for changes 
        if (this.currentUser.role.toString() === 'Administrator') {
          this.userService.getRole_(1);
        }
        else {
          this.userService.getRole_(0);
        }
      }
      else {
        this.userService.getRole_(0);
      } 
    });
  }

}
