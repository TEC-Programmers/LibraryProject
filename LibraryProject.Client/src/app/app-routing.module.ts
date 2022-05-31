import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorComponent } from './author/author.component';
import { CategoryBooksComponent } from './category-books/category-books.component';
import { CategoryComponent } from './category/category.component';
import { FrontpageComponent } from './frontpage/frontpage.component';

const routes: Routes = [
  {path: '', component : FrontpageComponent},   //route for the frontpage
  {path: 'Author', component:AuthorComponent},  //route of the author
  {path: 'Category', component:CategoryComponent},   // it displays the Category component
  { path: 'category_books/:id', component: CategoryBooksComponent },   //this is the url which displays the books of the  specific categoryID
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
