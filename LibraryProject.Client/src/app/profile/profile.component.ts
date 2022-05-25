import { User } from '../_models/User';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';


@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']

})
export class ProfileComponent implements OnInit {

  users: User[] = [];
  user: User = this.newUser();
  message: string[] = [];
  currentUser: User = { Id: 0, FirstName: '', MiddleName: '', LastName: '', Email: '', Password: ''};


  constructor(
    private router: Router,
    private authService: AuthService,
    private userService: UserService

  ) {
    // get the current user from authentication service
    this.authService.currentUser.subscribe(x => this.currentUser = x);
  }

  ngOnInit(): void {

  }

  newUser(): User {
    return { Id: 0,  FirstName: '', MiddleName: '', LastName: '', Email: '', Password: ''};
  }

  edit(user: User): void {
    this.user=  user;
    this.message = [];
  }

  cancel(): void {
    this.message = [];
    this.user= this.newUser();
  }

  save(): void {
    if (this.user.Email != '') {
     (confirm('To view the updated profile kindly "Sign in" again....!'))
    }
    this.message = [];

    if (this.user.Email == '') {
      this.message.push('Enter Email');
    }

    if (this.user.Password == '') {
      this.message.push('Enter Password');
    }

    if (this.user.FirstName== '') {
      this.message.push('Enter Firstname');
    }

    if (this.user.MiddleName == '') {
      this.message.push('Enter Middlename');
    }

    if (this.user.LastName== '') {
      this.message.push('Enter Lastname');
    }

  

    if (this.message.length == 0) {
      if (this.user.Id == 0) {
        this.userService.registerUser(this.user)
           .subscribe(a => {
          this.users.push(a)
          this.user= this.newUser();
          });
      } 
     /* else {
        this.userService.updateUser(this.user.Id, this.user)
          .subscribe(() => {
            this.user = this.newUser();
          });*/
        
         }
    }
  }
