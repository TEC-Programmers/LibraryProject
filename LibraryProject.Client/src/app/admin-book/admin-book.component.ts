import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import th from '@mobiscroll/angular/dist/js/i18n/th';
import { Author } from 'app/_models/Author';
import { Book } from 'app/_models/Book';
import { Category } from 'app/_models/Category';
import { Publisher } from 'app/_models/Publisher';
import { AuthorService } from 'app/_services/author.service';
import { BookService } from 'app/_services/book.service';
import { CategoryService } from 'app/_services/category.service';
import { PublisherService } from 'app/_services/publisher.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-admin-book',
  templateUrl: './admin-book.component.html',
  styleUrls: ['./admin-book.component.css']
})
export class AdminBookComponent implements OnInit {
  authors: Author[] = [];
  publishers: Publisher[] = [];
  categorys: Category[] = [];

  books: Book[] = [];
  book: Book = { id: 0, title: '', language: '', description: '', publishYear: 0, categoryId: 0, authorId: 0, publisherId: 0, image: '' };
  message: string = '';
  searchText!: string;

  selectedImg = null;
  imageArray = [
    {"name": "Book_Pippi.jpg"},
    {"name": "Book_Putgarden.jpg"},
    {"name": "Br¢drene_L¢vehjerte.jpeg"},
    {"name": "Emil_Fra_L¢nneberg.jpg"},
    {"name": "Romaner_Books.png"},
    {"name": "Tor_Fanger_Tyve.jpg"}
]

// "{ \"test\": \"test \"}"

  constructor(private httpClient: HttpClient, private bookService: BookService, private authorService: AuthorService, private publisherService: PublisherService, private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.bookService.getAllBooks().subscribe(x => this.books = x);
    this.authorService.getAllAuthors().subscribe(a => this.authors = a);
    this.publisherService.getAllPublishers().subscribe(p => this.publishers = p);
    this.categoryService.getAllCategories().subscribe(c => this.categorys = c);

    // this.httpClient.get('assets/images', {responseType: 'json'})
    // .subscribe(data => console.log('data: ',data))
  }

  getImages(): void {
  }
  

  edit_book(book: Book): void {
    this.message = '';
    this.book = book;
    this.book.id = book.id || 0;
    console.log(this.book);
  }

  delete_book(book: Book): void {
    if(confirm('Er du sikker på du vil slette?')) {
      this.bookService.deleteBook(book.id)
        .subscribe(() => {
            this.books = this.books.filter(x => x.id != book.id);
        });
    }
  }

  save_book(): void {
    console.log(this.book)
    this.message = '';

    if(this.book.id == 0) {
      for (var obj of this.imageArray) {
        let toStr = "";
        for (let key in obj) {
          if (obj.hasOwnProperty(key)) {
            toStr += `${key} ${obj[key]}` + ",  ";
            this.book.image = toStr;
          }
        }
      }
      this.bookService.addBook(this.book)
      .subscribe({
        next: (x) => {
          this.books.push(x);
          this.book = { id: 0, title: '', language: '', description: '',publishYear: 0, categoryId: 0, authorId: 0, publisherId: 0 };
          this.message = '';
          Swal.fire({
            title: 'Success!',
            text: 'Book added successfully',
            icon: 'success',
            confirmButtonText: 'Continue'
          });
        },
        error: (err) => {
          console.log(err.error);
          // this.message = Object.values(err.error.errors).join(", ");
        }
      }); 
    } else {
      this.bookService.updateBook(this.book.id, this.book)
      .subscribe({
        error: (err) => {
          console.log(err.error);
          // this.message = Object.values(err.error.errors).join(", ");
        },
        complete: () => {
          // this.message = '';
          this.book = { id: 0, title: '', language: '', description: '',publishYear: 0, categoryId: 0, authorId: 0, publisherId: 0 };
          Swal.fire({
            title: 'Success!',
            text: 'Book updated successfully',
            icon: 'success',
            confirmButtonText: 'Continue'
          });
        }
      });
    }
  }

  cancel(): void {
    this.book = { id: 0, title: '', language: '', description: '',publishYear: 0, categoryId: 0, authorId: 0, publisherId: 0 };
  }

}
