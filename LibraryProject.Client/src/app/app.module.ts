import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { AdministratorComponent } from './administrator/administrator.component';
import { AdminCustomerComponent } from './admin-customer/admin-customer.component';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { DatePipe } from '@angular/common';
import { CategoryComponent } from './category/category.component';
import { BookComponent } from './book/book.component';
import { LoginComponent } from './login/login.component';
import { ContactComponent } from './contact/contact.component';
import { CategoryDetailsComponent } from './category-details/category-details.component';
import { BookDetailsComponent } from './book-details/book-details.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import{ MatDatepickerModule} from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatNativeDateModule } from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';

@NgModule({
  declarations: [
    AppComponent,
    FrontpageComponent,
    AdministratorComponent,
    AdminCustomerComponent,
    CategoryComponent,
    BookComponent,
    LoginComponent,
    ContactComponent,
    CategoryDetailsComponent,
    BookDetailsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    Ng2SearchPipeModule,
    BrowserAnimationsModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatNativeDateModule,
    MatInputModule,
    MatIconModule
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
