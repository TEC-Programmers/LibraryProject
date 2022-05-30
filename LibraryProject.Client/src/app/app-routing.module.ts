import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdministratorComponent } from './administrator/administrator.component';
import { AuthorComponent } from './author/author.component';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { AuthGuard } from './_helpers/auth.guard';
import { Role } from './_models/User';
import { LoginComponent } from './login/login.component';
import { ProfileComponent } from './profile/profile.component';
import { CustomerComponent } from './customer/customer.component';


const routes: Routes = [
  {path: '', component : FrontpageComponent},
  {path: 'Author', component: AuthorComponent}, 
  {path: 'Admin', component: AdministratorComponent},
  {path: 'Admin/Customers', component: AdministratorComponent},
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard], data: { Roles: [Role.customer, Role.admin] } },
  { path: 'Login', component: LoginComponent },
  { path: 'customer', component: CustomerComponent, canActivate: [AuthGuard], data: { roles: [Role.admin] } },
  // {path: 'Admin/Books', component: AdministratorComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
