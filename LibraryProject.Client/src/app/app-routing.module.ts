import { NgModule } from '@angular/core';
import { Role } from './_models/Role';
import { RouterModule, Routes } from '@angular/router';
import { BookDetailsComponent } from './book-details/book-details.component';
import { CategoryBooksComponent } from './category-books/category-books.component';
import { CategoryComponent } from './category/category.component';
import { AdministratorComponent } from './administration/administrator/administrator.component';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { AuthGuard } from './_helpers/auth.guard';
import { LoginComponent } from './user/login/login.component';
import { ProfileComponent } from './user/profile/profile.component';
import { RegisterComponent } from './user/register/register.component';
import { LoanComponent } from './loan/loan.component';
import { AdminCustomerComponent } from './administration/admin-customer/admin-customer.component';
import { AdminBookComponent } from './administration/admin-book/admin-book.component';
import { AdminCategoryComponent } from './administration/admin-category/admin-category.component';
import { ReservationComponent } from './reservation/reservation.component';
import { CustomerPanelComponent } from './customer/customer-panel/customer-panel.component';
import { ActiveLoansComponent } from './customer/active-loans/active-loans.component';
import { ActiveReservationsComponent } from './customer/active-reservations/active-reservations.component';

const routes: Routes = [
  {path: '', redirectTo: '/Frontpage', pathMatch: 'full' },   //route for the frontpage
  {path: 'Frontpage', component: FrontpageComponent},
  {path: 'Category', component: CategoryComponent},   // it displays the Category component
  {path: 'category_books/:id', component: CategoryBooksComponent },  //this is the url which displays the books of the  specific categoryID
  {path: 'book_details/:id', component: BookDetailsComponent},
  {path: 'Admin', component: AdministratorComponent, canActivate: [AuthGuard]},
  {path: 'Admin/Customers', component: AdministratorComponent, canActivate: [AuthGuard]},
  {path: 'Admin/Users', component: AdminCustomerComponent, canActivate: [AuthGuard]},
  {path: 'Admin/Books', component: AdminBookComponent, canActivate: [AuthGuard]},
  {path: 'Admin/Categorys', component: AdminCategoryComponent},
  {path: 'profile', component: ProfileComponent},
  {path: 'customerpanel', component: CustomerPanelComponent },
  {path: 'activeLoans', component: ActiveLoansComponent},
  {path: 'activeReservations', component: ActiveReservationsComponent},
  {path: 'activeLoans/customerpanel', component: CustomerPanelComponent },
  {path: 'activeReservations/customerpanel', component: CustomerPanelComponent },
  {path: 'profile/customerpanel', component: CustomerPanelComponent },
  {path: 'customerpanel/activeReservations', component: ActiveReservationsComponent},
  {path: 'profile/customerpanel/activeReservations', component: ActiveReservationsComponent},
  {path: 'login', component: LoginComponent },
  {path: 'Register', component: RegisterComponent},
  {path: 'loan/:id', component: LoanComponent},
  {path: 'reserve/:id', component: ReservationComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
