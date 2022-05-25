import { Component } from '@angular/core';
import { Router}  from '@angular/router';
import { AuthService } from './_services/auth.service';
import { Role, User } from './_models/User';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {

  currentUser: User = { Id: 0, FirstName:'', MiddleName:'',LastName:'',Email:'',Password:''};

  title = 'LibraryProject-Client';

  constructor(
    private router: Router,
    private authService: AuthService,
   
  ) {
    // get the current user from authentication service
    this.authService.currentUser.subscribe(x => this.currentUser= x);
  }

  ngOnInit(): void{   

  }

  logout() {
    if (confirm('Are you sure you want to log out?')) {
      // ask authentication service to perform logout
      this.authService.logout();
      

      // subscribe to the changes in currentUser, and load Home component
      this.authService.currentUser.subscribe(x => {
        this.currentUser = x
        this.router.navigate(['/']);
      });
    }

  }
}
