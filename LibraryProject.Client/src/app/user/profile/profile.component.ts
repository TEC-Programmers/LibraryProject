import { User } from 'app/_models/User';
import { Component, ElementRef, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'app/_services/auth.service';
import { UserService } from 'app/_services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']

})

export class ProfileComponent implements OnInit {
  users: User[] = [];
  user: User = this.newUser();
  message: string[] = [];
  currentUser: User = { id: 0, firstName: '', middleName: '', lastName: '', email: '', password: '', role: 0};
  x:any;

  constructor(private router: Router, private authService: AuthService, private userService: UserService, private elementRef: ElementRef) {
    // get the current user from authentication service
    this.authService.currentUser.subscribe(x=> this.currentUser = x);
  }

  ngOnInit(): void {
    this.elementRef.nativeElement.ownerDocument.body.style.backgroundColor = '#607D8B';
    setTimeout(() => {
      this.showOrhideAdminBtn();
    });
  }

  ngAfterViewInit() {
    setTimeout(() => {
      this.showOrhideAdminBtn();
    });
  }

  showOrhideAdminBtn() {
    this.authService.currentUser.subscribe(user => {
    this.currentUser = user;
  
    if (this.x !== 1) {
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
    }
    });
  }

  newUser(): User {
    return { id: 0,  firstName: '', middleName: '', lastName: '', email: '', password: '', role: 0};
  }

  edit(user: User): void {
    this.user=  user;
    this.message = [];
  }

  cancel(): void {
    this.message = [];
    this.user = this.newUser();
  }

  save(): void {
    if (this.user.email != '') {
    }
    this.message = [];

    if (this.user.email == '') {
      this.message.push('Enter Email');
    }

    if (this.user.password == '') {
      this.message.push('Enter Password');
    }

    if (this.user.firstName== '') {
      this.message.push('Enter Firstname');
    }

    if (this.user.middleName == '') {
      this.message.push('Enter Middlename');
    }

    if (this.user.lastName== '') {
      this.message.push('Enter Lastname');
    }


    if (this.message.length == 0) {
      if (this.user.id == 0) {
        this.userService.registerUser(this.user)
           .subscribe(a => {
          this.users.push(a)
          this.user= this.newUser();
          });
      }
      else {
     (confirm('To view the updated profile kindly "Sign in" again....!'))
        this.userService.updateUser(this.user.id , this.user)
          .subscribe(() => {
            this.user = this.newUser();
          });

       }
    }
    
}}
