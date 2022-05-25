import { Component, OnInit, PipeTransform } from '@angular/core';
import { User } from '../_models/User';
import { UserService } from '../_services/user.service';
import {AfterViewInit, ViewChild} from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { FormControl } from '@angular/forms';
import { map, startWith } from 'rxjs';
import { DecimalPipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
// import {MatPaginator} from '@angular/material/paginator';
// import {MatTableDataSource} from '@angular/material/table';

@Component({
  selector: 'app-admin-customer',
  templateUrl: './admin-customer.component.html',
  styleUrls: ['./admin-customer.component.css']
})
export class AdminCustomerComponent implements OnInit {
  total_Users: User[] = [];
  Users: User[] = [];
  user: User = { id: 0, firstName: '', lastName: '', middleName: '', email: '', password: '', role: 0 }
  displayedColumns: string[] = ['Id', 'Firstname', 'Middlename', 'Lastname', 'Email', 'Role'];

  searchText!: string;

  constructor(private userService: UserService, private http: HttpClient) {}

  ngOnInit(): void {
    this.userService.getAllUsers().subscribe(u => this.total_Users = u);
    // chech user role
    // this.dtOptions = {
    //   pagingType: 'full_numbers'
    // };
  }

  

}
