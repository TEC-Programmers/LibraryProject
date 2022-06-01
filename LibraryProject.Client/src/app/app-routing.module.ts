import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdministratorComponent } from './administrator/administrator.component';
import { BookDetailsComponent } from './book-details/book-details.component';
import { BookComponent } from './book/book.component';
import { CategoryDetailsComponent } from './category-details/category-details.component';
import { CategoryComponent } from './category/category.component';
import { ContactComponent } from './contact/contact.component';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  {path: '', redirectTo: '/Frontpage', pathMatch: 'full'},
  {path: 'Frontpage', component: FrontpageComponent},
  {path: 'Admin', component: AdministratorComponent},
  {path: 'Admin/Customers', component: AdministratorComponent},
  {path: 'Category', component: CategoryComponent},
  {path: 'Category-Details', component: CategoryDetailsComponent},
  {path: 'Book', component:BookComponent},
  {path: 'Contact', component: ContactComponent},
  {path: 'Login', component: LoginComponent},
  {path: 'Book-Details', component: BookDetailsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
