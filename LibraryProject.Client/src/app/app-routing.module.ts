import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminBookComponent } from './admin-book/admin-book.component';
import { AdminCustomerComponent } from './admin-customer/admin-customer.component';
import { AdministratorComponent } from './administrator/administrator.component';
import { AuthorComponent } from './author/author.component';
import { FrontpageComponent } from './frontpage/frontpage.component';

const routes: Routes = [
  {path: '', component : FrontpageComponent},
  {path: 'Author', component: AuthorComponent}, 
  {path: 'Admin', component: AdministratorComponent},
  {path: 'Admin', component: AdministratorComponent},
  {path: 'Admin/Customers', component: AdminCustomerComponent},
  {path: 'Admin/Books', component: AdminBookComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
