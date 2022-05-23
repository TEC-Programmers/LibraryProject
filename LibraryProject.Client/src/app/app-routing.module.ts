import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorComponent } from './author/author.component';
import { FrontpageComponent } from './frontpage/frontpage.component';

const routes: Routes = [
  {path: '', component : FrontpageComponent},
  {path: 'Author', component:AuthorComponent},
  {path: 'Book/:id', component: FrontpageComponent},
  {path: 'Category/:id', component: FrontpageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
