import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { User } from 'app/_models/User';
import { UserService } from 'app/_services/user.service';
import Swal from 'sweetalert2';
import { AuthService } from '../_services/auth.service';

@Component({ templateUrl: 'login.component.html' })
export class LoginComponent implements OnInit {
  id:number = 0;
  email: string = '';
  password: string = '';
  submitted = false;
  error = '';
  username: number = 0;
  currentUser: User ={ id: 0, firstName: '', middleName: '', lastName: '', email: '', password: '', role: 0};
  x: number = 0;
 bookId:number=0;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private userService: UserService,
  ) {
    // redirect to home if already logged in
    if (this.authService.currentUserValue != null && this.authService.currentUserValue.id > 0) {
      this.router.navigate(['Frontpage']); 
    }
  }

  ngOnInit() {
    
   this.route.params.subscribe(params => {
      this.bookId = +params['id'];
    }); 
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

  login(): void {
    this.error = '';
    this.authService.login(this.email, this.password)
      .subscribe({
        next: () => {
          this.showOrhideAdminBtn();

          if (this.currentUser.role.toString() !== 'Administrator') {
            this.router.navigate(['Frontpage']);
          }
          else {
            this.router.navigate(['Admin']);
          }
          Swal.fire({
            title: 'Success!',
            text: 'Logged In Successfully',
            icon: 'success',
            confirmButtonText: 'Continue'
          });         
        },
        error: obj => {
          // console.log('login error ', obj.error);
          if (obj.error.status == 400 || obj.error.status == 401 || obj.error.status == 500) {
            this.error = 'Incorrect Username or Password';
          }
          else {
            this.error = obj.error.title;
          }
        }
      });
    
  }
  
}