import { Component, OnInit } from '@angular/core';
import { User } from 'app/_models/User';
import { UserService } from 'app/_services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  users: User[] = [];
  user: User = this.newUser();
  message: string[] = [];
  error = '';

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit(): void {
    if (this.getUsers.length > 0) {
      this.getUsers();
    }
  }

  newUser(): User {
    return this.user = { id: 0, firstName: '', middleName: '', lastName: '', email: '', password: '', role: 0 };
  }

  getUsers(): void {
    this.userService.getAllUsers()
      .subscribe(a => this.users = a);
  }

  cancel(): void {
    this.message = [];
    this.user = this.newUser();
  }

  save(): void {
    this.message = [];


    if (this.user.firstName == '') {
      this.message.push('Enter FirstName');
    }
    if (this.user.middleName== '') {
      this.message.push('Enter Middle Name');
    }
    if (this.user.lastName == '') {
      this.message.push('Enter Lastname');
    }
    if (this.user.email == '') {
      this.message.push('Email field cannot be empty');
    }
    if (this.user.password == '') {
      this.message.push('Password field cannot be empty');
    }


    if (this.message.length == 0) {
      if (this.user.id == 0) {
        this.userService.registerUser(this.user)
           .subscribe({
            next: a => {
            this.users.push(a)

            this.user = this.newUser();
            alert('Thanks for Signing Up!');
            this.router.navigate(['login']);
           },
           error: (err)=>{
                alert("User already exists!");
         }
         });
       } else {
            this.userService.updateUser(this.user.id, this.user)
              .subscribe(() => {
                this.user = this.newUser();
              });
           }
  }}
}


