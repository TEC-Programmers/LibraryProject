import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthorComponent } from './author/author.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { AdministratorComponent } from './administrator/administrator.component';
import { AdminCustomerComponent } from './admin-customer/admin-customer.component';
import { AngularPaginatorModule } from 'angular-paginator';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { DecimalPipe } from '@angular/common';
import { SearchFilterPipe } from './search-filter.pipe';
import { DataTablesModule } from 'angular-datatables';
import { AdminBookComponent } from './admin-book/admin-book.component';
import { listFiles } from 'list-files-in-dir';
import { AdminCategoryComponent } from './admin-category/admin-category.component'


@NgModule({
  declarations: [
    AppComponent,
    AuthorComponent,
    FrontpageComponent,
    AdministratorComponent,
    AdminCustomerComponent,
    SearchFilterPipe,
    AdminBookComponent,
    AdminCategoryComponent,
  ],
  imports: [ 
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    AngularPaginatorModule,
    Ng2SearchPipeModule,
    ReactiveFormsModule,
    DataTablesModule,
    ReactiveFormsModule,
  ],
  providers: [DecimalPipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
