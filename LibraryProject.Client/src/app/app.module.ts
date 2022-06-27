import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { CategoryComponent } from './category/category.component';
import { CategoryBooksComponent } from './category-books/category-books.component';
import { BookDetailsComponent } from './book-details/book-details.component';
import { AdministratorComponent } from './administrator/administrator.component';
import { AdminCustomerComponent } from './admin-customer/admin-customer.component';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { DatePipe } from '@angular/common';
import { BookComponent } from './book/book.component';
import { LoginComponent } from './login/login.component';
// import { ContactComponent } from './contact/contact.component';
import { LoanComponent } from './loan/loan.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { JwtInterceptor } from './_helpers/jwt.interceptor';    // autoinject JWT into all requests
import { ProfileComponent } from './profile/profile.component';
import { RegisterComponent } from './register/register.component';
import { Router } from '@angular/router';
import { NgxPaginationModule } from 'ngx-pagination';
import { AdminBookComponent } from './admin-book/admin-book.component';
import { AdminCategoryComponent } from './admin-category/admin-category.component';



@NgModule({
  declarations: [
    AppComponent,
    FrontpageComponent,
    AdministratorComponent,
    AdminCustomerComponent,
    ProfileComponent,
    RegisterComponent,
    CategoryComponent,
    BookComponent,
    LoginComponent,
    BookDetailsComponent,
    LoanComponent,
    CategoryBooksComponent,
    AdminBookComponent,
    AdminCategoryComponent

  ],

  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    Ng2SearchPipeModule,
    NgxPaginationModule
    ],

  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    [DatePipe]

  ],
  bootstrap: [AppComponent]


})
export class AppModule { }
