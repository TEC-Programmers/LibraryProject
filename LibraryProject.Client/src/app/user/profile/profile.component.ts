import { User } from 'app/_models/User';
import { Component, ElementRef, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'app/_services/auth.service';
import { UserService } from 'app/_services/user.service';
import { Role } from 'app/_models/Role';
import Swal from 'sweetalert2';

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
  showForm: Boolean = true;
  showPassword: boolean = false;
  toggle1: boolean = false;
  toggle2: boolean = false;
  confirmPass: string = '';

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

  // showHidePassword() {
  //   this.showPassword = !this.showPassword;
  // }

  changeType2(input_field_password, num){
    if(input_field_password.type=="password")
      input_field_password.type = "text";
    else
      input_field_password.type = "password";
  }

  changeType(input_field_password, num){
    if(input_field_password.type=="password")
      input_field_password.type = "text";
    else
      input_field_password.type = "password";

    if(num == 1)
      this.toggle1 = !this.toggle1;
    else
      this.toggle2 = !this.toggle2;
  }

  returnFromNewPass() {
    this.showForm = true;
    this.user = this.newUser();
    window.location.reload();
  }

  hideForm(user: User) {
    this.showForm = false;
    this.user = this.newUser();
    this.user = user;
    this.user.password = '';
    this.message = [];
    console.log('user: ',user)
  }

  showOrhideAdminBtn() {
    this.authService.currentUser.subscribe(x => {
    this.currentUser = x;
  
    if (this.currentUser) {
      this.userService.getRole$.subscribe(x => this.x = x ); // start listening for changes 
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

  newUser(): User {
    return { id: 0,  firstName: '', middleName: '', lastName: '', email: '', password: '', role: 0 };
  }

  edit(user: User): void {
    this.user = user;
    this.message = [];
  }

  cancelNewPass(): void {
    this.message = [];
    this.user.password = '';
    this.confirmPass = '';
  }

  cancel(): void {
    this.message = [];
    this.user = this.newUser();
  }

  savePassword(): void {
    this.message = [];

    if (this.confirmPass !== this.user.password){
      this.message.push(" Passwords must match")
    }

    if (this.user.password.length < 6) {
      this.message.push(' Password must be atleast 6 characters');
    }

    if (this.message.length == 0) {
      if (this.user.id !== 0) {
        if((confirm('Are you sure to reset password?'))) {
          if(this.user.role.toString() === 'Customer') {
            console.log('Customer: ',this.user)
            this.userService.updateUser(this.user.id, this.user)
                .subscribe(() => {
                  this.user = this.newUser();
                    this.showForm = true;
                    Swal.fire({
                      title: 'Success!',
                      text: 'Password updated Successfully',
                      icon: 'success',
                      confirmButtonText: 'Continue'
                    }); 
                });
          }
          else if(this.user.role.toString() === 'Administrator') {
            console.log('Administrator: ',this.user)
            this.userService.updateUser(this.user.id, this.user)
                .subscribe(() => {
                  this.user = this.newUser();
                  this.showForm = true;
                  Swal.fire({
                    title: 'Success!',
                    text: 'Password updated Successfully',
                    icon: 'success',
                    confirmButtonText: 'Continue'
                  });  
                });
          }
        }
        else {
          this.message.push('Password reset canceled by user')
          this.user.password = '';
          this.confirmPass = '';
        }
      }
    }
  }

  save(): void {
    this.message = [];

    if (this.user.email == '') {
      this.message.push(' Enter Email');
    }

    if (this.user.password == '') {
      this.message.push(' Enter Password');
    }

    if (this.user.firstName == '') {
      this.message.push(' Enter Firstname');
    }

    if (this.user.middleName == '') {
      this.message.push(' Enter Middlename');
    }

    if (this.user.lastName == '') {
      this.message.push(' Enter Lastname');
    }

    if (this.message.length == 0) {
      if (this.user.id == 0) {
        this.userService.registerUser(this.user)
           .subscribe(a => {
          this.users.push(a)
          this.user= this.newUser();
          });
      }
      else if ((confirm('Update profile?')))
      {    
        if(this.user.role.toString() === 'Customer') {
          console.log('True customer')
          console.log('cus user: ',this.user)
          this.userService.updateUser(this.user.id, this.user)
              .subscribe(() => {
                this.user = this.newUser();
              });
        }
        else if(this.user.role.toString() === 'Administrator') {
          console.log('True admin')
          console.log('admin user: ',this.user)
          this.userService.updateUser(this.user.id, this.user)
              .subscribe(() => {
                this.user = this.newUser();
              });
        }
        
      }
      else {
        this.message.push('Update user canceled')
        this.user = this.newUser();
      }
    }
    
}}
