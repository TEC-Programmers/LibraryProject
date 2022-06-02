import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthorComponent } from './author/author.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { AdministratorComponent } from './administrator/administrator.component';
import { AdminCustomerComponent } from './admin-customer/admin-customer.component';
import { CustomerComponent } from './customer/customer.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './_helpers/auth.guard';
import { JwtInterceptor } from './_helpers/jwt.interceptor';
import { ProfileComponent } from './profile/profile.component';
import { RegisterComponent } from './register/register.component';


@NgModule({
  declarations: [
    AppComponent,
    AuthorComponent,
    FrontpageComponent,
    AdministratorComponent,
    AdminCustomerComponent,
    CustomerComponent,
    LoginComponent,
    ProfileComponent,
    RegisterComponent,


  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
