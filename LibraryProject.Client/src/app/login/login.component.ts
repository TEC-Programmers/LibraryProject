import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../_services/auth.service';

@Component({ templateUrl: 'login.component.html' })
export class LoginComponent implements OnInit {
  id:number=0;
  email: string = '';
  password: string = '';
  submitted = false;
  error = '';
  username: number = 0;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
   // private cartService:CartService
  ) {
    // redirect to home if already logged in
    // console.log(this.authenticationService.currentUserValue);
    if (this.authService.currentUserValue != null && this.authService.currentUserValue.id> 0) {
      this.router.navigate(['login']);
     
    }
  }

  ngOnInit() {
   this.username = this.authService.currentUserValue.id; 
  }

  login(): void {
    this.error = '';
    this.authService.login(this.email, this.password)
      .subscribe({
        next: () => {
          // // get return url from route parameters or default to '/'
          const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
          
          
          this.router.navigate(['returnUrl']);
          var username = this.authService.currentUserValue.firstName; 
        
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