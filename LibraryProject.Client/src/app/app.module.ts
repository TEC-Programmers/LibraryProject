import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthorComponent } from './author/author.component';
import { FormsModule } from '@angular/forms';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { CategoryComponent } from './category/category.component';
import { CategoryBooksComponent } from './category-books/category-books.component';
import { BookDetailsComponent } from './book-details/book-details.component';


@NgModule({
  declarations: [
    AppComponent,
    AuthorComponent,
    FrontpageComponent,
    CategoryComponent,
    CategoryBooksComponent,
    BookDetailsComponent


  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
