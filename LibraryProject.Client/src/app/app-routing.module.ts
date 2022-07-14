import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookDetailsComponent } from './book-details/book-details.component';
import { CategoryBooksComponent } from './category-books/category-books.component';
import { CategoryComponent } from './category/category.component';
import { AdministratorComponent } from './administrator/administrator.component';
import { BookComponent } from './book/book.component';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { ProfileComponent } from './profile/profile.component';
import { RegisterComponent } from './register/register.component';


import { LoanComponent } from './loan/loan.component';
import { LoginComponent } from './login/login.component';
import { Role } from './_models/Role';
import { AuthGuard } from './_helpers/auth.guard';
import { AdminCustomerComponent } from './admin-customer/admin-customer.component';
import { AdminBookComponent } from './admin-book/admin-book.component';
import { AdminCategoryComponent } from './admin-category/admin-category.component';
import { ReservationComponent } from './reservation/reservation.component';



const routes: Routes = [
  { path: '', redirectTo: '/Frontpage', pathMatch: 'full' },   //route for the frontpage
  {path: 'Frontpage', component: FrontpageComponent},
  {path: 'Category', component:CategoryComponent},   // it displays the Category component
  { path: 'category_books/:id', component: CategoryBooksComponent },  //this is the url which displays the books of the  specific categoryID
  {path: 'book_details/:id', component: BookDetailsComponent},
  {path: 'Admin', component: AdministratorComponent},
  {path: 'Admin/Customers', component: AdministratorComponent},
  {path: 'Admin/Books', component: AdminBookComponent},
  {path: 'Admin/Users', component: AdminCustomerComponent},
  {path: 'Admin/Categorys', component: AdminCategoryComponent},
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard], data: { Roles: [Role.Customer, Role.Administrator] } },
  { path: 'login', component: LoginComponent },
  { path: 'Register', component: RegisterComponent},

  {path: 'Book', component:BookComponent},
  {path: 'loan/:id', component: LoanComponent},
 // { path: 'customer', component: CustomerComponent, canActivate: [AuthGuard], data: { roles: [Role.admin] } },
  // {path: 'Admin/Books', component: AdministratorComponent},
  {path: 'Book/:filterTerm', component:FrontpageComponent},
  {path: 'loan/:id', component: LoanComponent},
  {path: 'reserve/:id', component: ReservationComponent},
  

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
