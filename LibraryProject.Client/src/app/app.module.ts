import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { CategoryComponent } from './category/category.component';
import { CategoryBooksComponent } from './category-books/category-books.component';
import { BookDetailsComponent } from './book/book-details/book-details.component';
import { AdministratorComponent } from './administration/administrator/administrator.component';
import { AdminCustomerComponent } from './administration/admin-customer/admin-customer.component';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { DatePipe } from '@angular/common';
import { LoginComponent } from './user/login/login.component';
import { LoanComponent } from './loan/loan.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatInputModule} from '@angular/material/input';
import {MatNativeDateModule} from '@angular/material/core'

import { JwtInterceptor } from './_helpers/jwt.interceptor';    // autoinject JWT into all requests
import { ProfileComponent } from './user/profile/profile.component';
import { RegisterComponent } from './user/register/register.component';
import { Router } from '@angular/router';
import { NgxPaginationModule } from 'ngx-pagination';
import { AdminBookComponent } from './administration/admin-book/admin-book.component';
import { AdminCategoryComponent } from './administration/admin-category/admin-category.component';
import { ReservationComponent } from './reservation/reservation.component';
import { CustomerPanelComponent } from './customer/customer-panel/customer-panel.component';
import { ActiveLoansComponent } from './customer/active-loans/active-loans.component';
import { ActiveReservationsComponent } from './customer/active-reservations/active-reservations.component';


@NgModule({
  declarations: [
    AppComponent,
    FrontpageComponent,
    AdministratorComponent,
    AdminCustomerComponent,
    ProfileComponent,
    RegisterComponent,
    CategoryComponent,
    LoginComponent,
    BookDetailsComponent,
    LoanComponent,
    CategoryBooksComponent,
    AdminBookComponent,
    AdminCategoryComponent,
    ReservationComponent,
    CustomerPanelComponent,
    ActiveLoansComponent,
    ActiveReservationsComponent,
  ],

  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    Ng2SearchPipeModule,
    MatDatepickerModule,
    MatInputModule,
    MatNativeDateModule,
    NgxPaginationModule
    ],

  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    [DatePipe]

  ],
  bootstrap: [AppComponent]


})
export class AppModule { }
