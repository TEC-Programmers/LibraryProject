import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdministratorComponent } from './administrator/administrator.component';
import { AuthorComponent } from './author/author.component';
import { CategoryComponent } from './category/category.component';
import { FrontpageComponent } from './frontpage/frontpage.component';

const routes: Routes = [
  {path: '', component : FrontpageComponent},
  {path: 'Author', component: AuthorComponent}, 
  {path: 'Admin', component: AdministratorComponent},
  {path: 'Admin/Customers', component: AdministratorComponent},
  {path: 'Category', component: CategoryComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
