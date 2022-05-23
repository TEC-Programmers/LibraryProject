import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorComponent } from './author/author.component';
import { CategoryBooksComponent } from './category-books/category-books.component';
import { CategoryComponent } from './category/category.component';
import { FrontpageComponent } from './frontpage/frontpage.component';

const routes: Routes = [
  {path: '', component : FrontpageComponent},
  {path: 'Author', component:AuthorComponent},
  {path: 'Category', component:CategoryComponent},
  { path: 'category_books/:id', component: CategoryBooksComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
