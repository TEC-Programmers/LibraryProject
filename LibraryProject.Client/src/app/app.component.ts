import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Book } from './_models/Book';
import { Category } from './_models/Category';
import { BookService } from './_services/book.service';
import { CategoryService } from './_services/category.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'LibraryProject-Client';

  book!: Book;
  counter = 0;
  total: number = 0;
  categories: Category[] = [];
  allBooks: Book[] = [];
  filterTerm!: string;


  constructor(private bookService: BookService, private categoryService: CategoryService) {}
  ngOnInit(): void {
    this.categoryService.getAllCategories()
    .subscribe(c => this.categories = c);

  }
showSearch(): void {

    if (this.filterTerm == null || this.filterTerm == '') {
     alert("The input field is empty")
    }

    else if (this.filterTerm.length >= 0 ){
      this.bookService.getAllBooks()
    .subscribe(p => this.allBooks = p);
    console.log(this.allBooks)
    }


  }
  click(){
      if (this.filterTerm == null || this.filterTerm == '') {
        alert("The input field is empty")
       }

       else if (this.filterTerm.length >= 0 ){
         this.bookService.getAllBooks()
       .subscribe(p => this.allBooks = p);
       console.log(this.allBooks)
       }
    }
    checkSearch(event:any){
      if (event.key === "Backspace" || this.filterTerm == null) {
        this.allBooks = [];
        console.log(event);
      }
    }

//   pageYoffset = 0;
//   @HostListener('window:scroll', ['$event']) onScroll(event){
//     this.pageYoffset = window.pageYOffset;
//  }
  // @ViewChild('scroll')
  // scroll!: ElementRef;

  // categories: Category[] = [];



  

 /* scrollToTop(){
  // this.scroll.scrollToPosition([0,0]);
  this.scroll.nativeElement.scrollToTop = 0;
}

scrollToBottom(){
  console.log(this.scroll.nativeElement.scrollHeight)
  this.scroll.nativeElement.scrollToTop = this.scroll.nativeElement.scrollHeight;
} */

}
