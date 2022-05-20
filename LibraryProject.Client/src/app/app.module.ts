import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthorComponent } from './author/author.component';
import { FormsModule } from '@angular/forms';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { DatePipe } from '@angular/common';
import { CategoryComponent } from './category/category.component';
@NgModule({
  declarations: [
    AppComponent,
    AuthorComponent,
    FrontpageComponent,
    CategoryComponent,


  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    Ng2SearchPipeModule
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
