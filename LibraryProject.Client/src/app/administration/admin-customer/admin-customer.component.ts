import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from '../../_models/User';
import { Role } from 'app/_models/Role';
import { UserService } from '../../_services/user.service';
import { HttpClient } from '@angular/common/http';
import Swal from 'sweetalert2'
import { UntypedFormBuilder } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, MatPaginatorIntl } from '@angular/material/paginator';
import { AuthService } from 'app/_services/auth.service';

@Component({
  selector: 'app-admin-customer',
  templateUrl: './admin-customer.component.html',
  styleUrls: ['./admin-customer.component.css']
})
export class AdminCustomerComponent implements OnInit {
  customers: User[] = [];
  customer: User = { id: 0, firstName: '', lastName: '', middleName: '', email: '', password: '', role: 0 }

  administrators: User[] = [];
  _administrators: User[] = [];
  _administrator: User = { id: 0, firstName: '', lastName: '', middleName: '', email: '', password: '', role: 0 }
  administrator: User = { id: 0, firstName: '', lastName: '', middleName: '', email: '', password: '', role: 0 }

  Users: User[] = [];
  total_users: User[] = [];

  displayedColumns: string[] = ['Operation', 'Firstname', 'Middlename', 'Lastname', 'Email'];
  searchText!: string;
  closeResult!: string;

  message: string = '';
  roles!: Role
  selectedValue = 1;
  p: any;
  x:any;
  currentUser: User = { id: 0, firstName: '', middleName: '', lastName: '', email: '', password: '', role: 0};
  getRole: Role = 0;
  resetRole: boolean = false;
  toggle1: boolean = false;
  toggle2: boolean = false;
  password: string = '';

  constructor(private userService: UserService, private http: HttpClient, private formBuilder: UntypedFormBuilder, private authService: AuthService) {}

  ngOnInit(): void {
    setTimeout(() => {
      this.showOrhideAdminBtn();
    });
    this.getAllCustomers();
    this.getAllAdmins();
  }

  ngAfterViewInit() {
    setTimeout(() => {
      this.showOrhideAdminBtn();
    });
  }


  edit_member(customer: User): void {
    this.message = '';
    this.customer = customer;
    this.customer.id = customer.id || 0;
  }

  edit_admin(administrator: User): void {
    this.message = '';
    this.administrator = administrator;
    this.password = administrator.password;
    this.administrator.id = administrator.id || 0;
  }



  delete_member(customer: User): void {
    if (confirm('Delete Customer: '+customer.firstName+' '+customer.middleName+' '+customer.lastName+'?')) {
      this.userService.deleteUser(customer.id)
      .subscribe(() => {
        this.customers = this.customers.filter(cus => cus.id != customer.id)
      })
    }
  }

  delete_admin(admin: User): void {
    if (confirm('Delete Admin: '+admin.firstName+' '+admin.middleName+' '+admin.lastName+'?')) {
      this.userService.deleteUser(admin.id)
      .subscribe(() => {
        this.administrators = this.administrators.filter(adm => adm.id != admin.id)
      })
    }
  }


  save_admin(): void {
    this.message = '';
    if(this.administrator.id == 0) {
      console.log('[Administrator] (NOT) Found...')
    } else {
      this.userService.updatePasswordWithProcedure(this.administrator.id, this.administrator)
      .subscribe({
        error: (err) => {
          console.log(err.error);
          this.message = Object.values(err.error.errors).join(", ");
        },
        complete: () => {
          this.message = '';
          this.administrator =  { id: 0, firstName: '', lastName: '', middleName: '', email: '', password: '', role: 0 };
          Swal.fire({
            title: 'Success!',
            text: 'Administrator updated successfully',
            icon: 'success',
            confirmButtonText: 'Continue'
          });
        }
      });
    }
  }


  save_member(): void {
    this.message = '';
    if(this.customer.id == 0) {
      console.log('[Customer] (NOT) Found...')
    } else {
      console.log('user before update: ',this.customer)
      this.userService.updateRole(this.customer.id, this.customer)
      .subscribe({
        error: (err) => {
          console.log(err.error);
          this.message = Object.values(err.error.errors).join(", ");
        },
        complete: () => {
          this.message = '';
          this.customer =  { id: 0, firstName: '', lastName: '', middleName: '', email: '', password: '', role: 0 };
          Swal.fire({
            title: 'Success!',
            text: 'Customer updated successfully',
            icon: 'success',
            confirmButtonText: 'Continue'
          });
          window.location.reload();
        }
      });
    }
  }

  showHidePassword() {
    this.resetRole = !this.resetRole;
  }

  
  resetRoleType(currentRole, num){
    if(currentRole.value == "Administrator") 
      currentRole.value = "Customer";  
    else 
      currentRole.value = "Customer";

    if(num == 1)
      this.toggle1 = !this.toggle1;
    else
      this.toggle2 = !this.toggle2;
  }


    getAllCustomers() {
      this.userService.getAllUsers().subscribe({
        next: (all_users) => {
          this.total_users = all_users;
          const indexOf_Customer = Object.values(Role).indexOf(1 as unknown as Role);
          const key = Object.keys(Role)[indexOf_Customer];

          this.customers = this.total_users.filter((obj) => {
            return obj.role.toString() === key        
          });
        },
        error: (err: any) => {
          console.log(err);
        },
        complete: () => {
          console.log('getAllCustomers() - Completed Successfully');
        },
      });
    }


    getAllAdmins() {
      this.userService.getAllUsers().subscribe({
        next: (all_users) => {
          this.total_users = all_users;

          const indexOf_Customer = Object.values(Role).indexOf(0 as unknown as Role);
          const key = Object.keys(Role)[indexOf_Customer];

          this.administrators = this.total_users.filter((obj) => {
            return obj.role.toString() === key
          });
        },
        error: (err: any) => {
          console.log(err);
        },
        complete: () => {
          console.log('getAllAdmins() - Completed Successfully');
        },
      });
    }

    showOrhideAdminBtn() {
      this.authService.currentUser.subscribe(user => {
      this.currentUser = user;
    
      if (this.x !== 1) {
        if (this.currentUser) {
          this.userService.getRole$.subscribe(x => this.x = x); // start listening for changes 
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
      }
      });
    }
}
