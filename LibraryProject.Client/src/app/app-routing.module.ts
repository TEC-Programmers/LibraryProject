import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookDetailsComponent } from './book-details/book-details.component';
import { CategoryBooksComponent } from './category-books/category-books.component';
import { CategoryComponent } from './category/category.component';
import { AdministratorComponent } from './administrator/administrator.component';
import { BookComponent } from './book/book.component';
import { ContactComponent } from './contact/contact.component';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { AuthGuard } from './_helpers/auth.guard';
import { LoginComponent } from './login/login.component';
import { ProfileComponent } from './profile/profile.component';
//import { CustomerComponent } from './customer/customer.component';
import { RegisterComponent } from './register/register.component';

import { LoanComponent } from './loan/loan.component';
import { AdminBookComponent } from './admin-book/admin-book.component';
import { AdminCategoryComponent } from './admin-category/admin-category.component';
import { Role } from './_models/Role';
import { AdminCustomerComponent } from './admin-customer/admin-customer.component';


const routes: Routes = [
  { path: '', redirectTo: '/Frontpage', pathMatch: 'full' },   //route for the frontpage
  {path: 'Frontpage', component: FrontpageComponent},
  {path: 'Category', component:CategoryComponent},   // it displays the Category component
  { path: 'category_books/:id', component: CategoryBooksComponent },  //this is the url which displays the books of the  specific categoryID
  {path: 'book_details/:id', component: BookDetailsComponent},
  {path: 'Book-Details/:id', component: BookDetailsComponent},
  {path: 'Admin', component: AdministratorComponent},
  {path: 'Admin/Users', component: AdminCustomerComponent},
  {path: 'Admin/Books', component: AdminBookComponent},
  {path: 'Admin/Categorys', component: AdminCategoryComponent},
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard], data: { Roles: [Role.Customer, Role.Administrator] } },
  { path: 'Login', component: LoginComponent },
  { path: 'Register', component: RegisterComponent},
 // { path: 'customer', component: CustomerComponent, canActivate: [AuthGuard], data: { roles: [Role.admin] } },
  // {path: 'Admin/Books', component: AdministratorComponent},
  {path: 'Book', component:BookComponent},
  {path: 'Contact', component: ContactComponent},
  {path: 'loan/:id', component: LoanComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
