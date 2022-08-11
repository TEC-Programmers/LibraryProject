import { Component, OnInit } from '@angular/core';
import { ActivatedRoute,Router } from '@angular/router';
import { Book } from '../_models/Book';
import { BookService } from '../_services/book.service';
import { Ng2SearchPipeModule } from 'ng2-search-filter';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.css']
})
export class BookComponent implements OnInit {

  searchTerm:string ="";
  books: Book[] = [];
  searchKey: string = "";
  searchBooks: Book[] = [];
  constructor(private bookService: BookService, private route:ActivatedRoute, private router:Router) { }


  ngOnInit(): void {
     this.bookService.getAllBooks().subscribe(x =>{
      this.books = x;
      this.searchBooks=this.books;


      this.bookService.search.subscribe((value: string) => {

        this.searchKey = value
        console.log(this.searchBooks, this.books);
        this.searchBooks = this.books.filter(x =>
          x.title.toLowerCase().includes(this.searchKey.toLowerCase()) || x.description.toLowerCase().includes(this.searchKey.toLowerCase())

      )});
    }); 
    this.route.queryParams.subscribe(params => {
      if(params['searchTerm'])
      this.searchTerm=params['searchTerm'];
    })
  }
  search():void{

    if(this.searchTerm)
    this.router.navigate(['/book',this.searchTerm]);
  }

}
