import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthService } from './_services/auth.service';
import { Role, User } from './_models/User';
;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  currentCustomer: User = { Id: 0, FirstName:'', MiddleName:'',LastName:'',Email:'',Password:'',Role:Role.Customer};

  title = 'LibraryProject-Client';

  constructor(
    
    private authService: AuthService,
   
  ) {
    // get the current customer from authentication service
    this.authService.currentUser.subscribe(x => this.currentCustomer = x);
  }

  logout() {
    if (confirm('Are you sure you want to log out?')) {
      // ask authentication service to perform logout
      this.authService.logout();
      

      // subscribe to the changes in currentUser, and load Home component
      this.authService.currentUser.subscribe(x => {
        this.currentCustomer = x
       // this.router.navigate(['/']);
      });
    }

  }
}
