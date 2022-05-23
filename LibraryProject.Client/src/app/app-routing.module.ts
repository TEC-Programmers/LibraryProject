import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdministratorComponent } from './administrator/administrator.component';
import { AuthorComponent } from './author/author.component';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { AuthGuard } from './_helpers/auth.guard';
import { Role } from './_models/User';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  {path: '', component : FrontpageComponent},
  {path: 'Author', component: AuthorComponent}, 
  {path: 'Admin', component: AdministratorComponent},
  {path: 'Admin/Customers', component: AdministratorComponent},
  { path: 'Login', component: LoginComponent },
  // {path: 'Admin/Books', component: AdministratorComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
