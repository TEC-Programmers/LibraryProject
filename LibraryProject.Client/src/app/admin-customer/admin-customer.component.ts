import { Component, OnInit, PipeTransform } from '@angular/core';
import { User } from '../_models/User';
import { UserService } from '../_services/user.service';
import { AfterViewInit, ViewChild } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { FormControl } from '@angular/forms';
import { map, startWith } from 'rxjs';
import { DecimalPipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Role } from '../_models/Role';
// import {MatPaginator} from '@angular/material/paginator';
// import {MatTableDataSource} from '@angular/material/table';
// import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-admin-customer',
  templateUrl: './admin-customer.component.html',
  styleUrls: ['./admin-customer.component.css']
})
export class AdminCustomerComponent implements OnInit {
  user: User = { id: 0, firstName: '', lastName: '', middleName: '', email: '', password: '', role: 0 }
  Users: User[] = [];
  total_users: User[] = [];
  customers: User[] = [];
  displayedColumns: string[] = ['Operation', 'Firstname', 'Middlename', 'Lastname', 'Email', 'Role'];
  searchText!: string;

  closeResult!: string;
  // private modalService: NgbModal

  constructor(private userService: UserService, private http: HttpClient) {}

  ngOnInit(): void { 
    this.getAllCustomers();
  }

  delete(customer: User): void {
    if (confirm('Delete Customer?')) {
      this.userService.deleteUser(this.user.id)
      .subscribe(() => {
        this.customers = this.customers.filter(cus => cus.id != customer.id)
      })
    }
  }









    getAllCustomers() { 
      this.userService.getAllUsers().subscribe({
        next: (all_users) => {
          this.total_users = all_users;

          const indexOf_Customer = Object.values(Role).indexOf(1 as unknown as Role);
          const key = Object.keys(Role)[indexOf_Customer];
          console.log('key: ',key)
      
          this.customers = this.total_users.filter((obj) => {
            return obj.role.toString() === key
          });

          console.log('customers: ',this.customers)
        },
        error: (err: any) => {
          console.log(err);
        },
        complete: () => {
          console.log('getAllCustomers() - Completed Successfully');
        },
      });
    }

}
