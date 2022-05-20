import { Component, OnInit } from '@angular/core';
import { User } from '../_models/User';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-admin-customer',
  templateUrl: './admin-customer.component.html',
  styleUrls: ['./admin-customer.component.css']
})
export class AdminCustomerComponent implements OnInit {
  total_Users: User[] = [];
  user: User = { id: 0, firstName: '', lastName: '', middleName: '', email: '', password: '', role: 0 }


  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.getAllUsers().subscribe(u => this.total_Users = u);
    // chech user role
  }

}
