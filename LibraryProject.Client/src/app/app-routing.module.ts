import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorComponent } from './author/author.component';
import { BookDetailsComponent } from './book-details/book-details.component';
import { CategoryBooksComponent } from './category-books/category-books.component';
import { CategoryComponent } from './category/category.component';
import { AdministratorComponent } from './administrator/administrator.component';
import { BookComponent } from './book/book.component';
import { CategoryDetailsComponent } from './category-details/category-details.component';
import { ContactComponent } from './contact/contact.component';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { LoanComponent } from './loan/loan.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  { path: '', redirectTo: '/Frontpage', pathMatch: 'full' },   //route for the frontpage
  {path: 'Author', component:AuthorComponent},  //route of the author
  {path: 'Category', component:CategoryComponent},   // it displays the Category component
  { path: 'category_books/:id', component: CategoryBooksComponent },  //this is the url which displays the books of the  specific categoryID
  { path: 'book_details/:id', component: BookDetailsComponent },
  {path: 'Admin', component: AdministratorComponent},
  {path: 'Admin/Customers', component: AdministratorComponent},

  {path: 'Category-Details', component: CategoryDetailsComponent},
  {path: 'Book', component:BookComponent},
  {path: 'Contact', component: ContactComponent},
  {path: 'Login', component: LoginComponent},
  {path: 'Book-Details', component: BookDetailsComponent},
  {path: 'Loan', component: LoanComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
