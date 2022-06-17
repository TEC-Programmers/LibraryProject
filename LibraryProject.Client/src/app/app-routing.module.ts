import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminBookComponent } from './admin-book/admin-book.component';
import { AdminCategoryComponent } from './admin-category/admin-category.component';
import { AdminCustomerComponent } from './admin-customer/admin-customer.component';
import { AdministratorComponent } from './administrator/administrator.component';
import { AuthorComponent } from './author/author.component';
import { CategoryComponent } from './category/category.component';
import { FrontpageComponent } from './frontpage/frontpage.component';

const routes: Routes = [
  {path: '', component : FrontpageComponent},
  {path: 'Author', component: AuthorComponent}, 
  {path: 'Admin', component: AdministratorComponent},
  {path: 'Admin', component: AdministratorComponent},
  {path: 'Admin/Users', component: AdminCustomerComponent},
  {path: 'Admin/Books', component: AdminBookComponent},
  {path: 'Admin/Categorys', component: AdminCategoryComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
