import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdministratorComponent } from './administrator/administrator.component';
import { BookComponent } from './book/book.component';
import { CategoryComponent } from './category/category.component';
import { ContactComponent } from './contact/contact.component';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  {path: 'Frontpage', component : FrontpageComponent},
  {path: 'Admin', component: AdministratorComponent},
  {path: 'Admin/Customers', component: AdministratorComponent},
  {path: 'Category', component: CategoryComponent},
  {path: 'Book', component:BookComponent},
  {path: 'Contact', component: ContactComponent},
  {path: 'Login', component: LoginComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
