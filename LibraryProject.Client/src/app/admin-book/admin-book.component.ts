import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Author } from 'app/_models/Author';
import { Book } from 'app/_models/Book';
import { Category } from 'app/_models/Category';
import { Publisher } from 'app/_models/Publisher';
import { User } from 'app/_models/User';
import { AuthService } from 'app/_services/auth.service';
import { AuthorService } from 'app/_services/author.service';
import { BookService } from 'app/_services/book.service';
import { CategoryService } from 'app/_services/category.service';
import { PublisherService } from 'app/_services/publisher.service';
import { UserService } from 'app/_services/user.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-admin-book',
  templateUrl: './admin-book.component.html',
  styleUrls: ['./admin-book.component.css']
})
export class AdminBookComponent implements OnInit {
  authors: Author[] = [];
  author: Author = { id: 0, firstName: '', middleName: '', lastName: ''}
  publishers: Publisher[] = [];
  publisher: Publisher = { id: 0, name: ''}
  books: Book[] = [];
  book: Book = { id: 0, title: '', language: '', description: '', publishYear: 0, categoryId: 0, authorId: 0, publisherId: 0, image: '', category: [], publisher: { id: 0, name: ''}, author: { id: 0, firstName: '', lastName: ''} };
  categorys: Category[] = [];
  isShown_author: boolean = true;
  isShown_publisher: boolean = true;
  isShown_category: boolean = true;
  isShown_image: boolean = true;

  isShown_author_form: boolean = false;
  btn_new_author: boolean = true;
  author_dropdown: boolean = true;
  authorId_value: boolean = false;

  isShown_publisher_form: boolean = false;
  btn_new_publisher: boolean = true;
  publisher_dropdown: boolean = true;
  publisherId_value: boolean = false;

  message: string = '';
  searchText!: string;
  p: any;
  x:any;
  category: Category = { id: 0, categoryName: '' }
  showErrorMess: boolean = true;
  showCreateBtn: boolean = false;
  showAuthorContinueBtn: boolean = false;
  disable_author: boolean = false;
  disable_publisher: boolean = false;
  currentUser: User ={ id: 0, firstName: '', middleName: '', lastName: '', email: '', password: '', role: 0};
  selectedImg = null;
  imageArray = [
    {"name": "Book_Pippi.jpg"},
    {"name": "Book_Putgarden.jpg"},
    {"name": "Br¢drene_L¢vehjerte.jpeg"},
    {"name": "Emil_Fra_L¢nneberg.jpg"},
    {"name": "Romaner_Books.png"},
    {"name": "Tor_Fanger_Tyve.jpg"}

  ]


  constructor(private userService: UserService, private authService: AuthService, private httpClient: HttpClient, private bookService: BookService, private authorService: AuthorService, private publisherService: PublisherService, private categoryService: CategoryService) { }

  ngOnInit(): void {
    setTimeout(() => {
      this.showOrhideAdminBtn();
    });
    this.bookService.getAllBooks().subscribe(x => this.books = x);
    this.authorService.getAllAuthors().subscribe(a => this.authors = a);
    this.publisherService.getAllPublishers().subscribe(p => this.publishers = p);
    this.categoryService.getAllCategories().subscribe(c => this.categorys = c);
    console.log('admin-book ngOnInit | x = ',this.x)

  }

  ngAfterViewInit() {
    setTimeout(() => {
      this.showOrhideAdminBtn();
    });
  }

  showOrhideAdminBtn() {
    this.authService.currentUser.subscribe(user => {
    this.currentUser = user;

    if (this.x !== 1) {
      if (this.currentUser) {
        this.userService.getRole$.subscribe(x => this.x = x); // start listening for changes
          if (this.currentUser.role.toString() === 'Administrator') {
            this.userService.getRole_(1);
          }
          else {
            this.userService.getRole_(0);
          }
        }
        else {
          this.userService.getRole_(0);
        }
    }
    });

    console.log('x line 97: ',this.x)
  }

  newPublisher(): void {
    this.isShown_publisher = false;
    this.isShown_category = false;
    this.isShown_image = false;
    this.isShown_publisher_form = true;
    this.publisher_dropdown = true;
    this.btn_new_publisher = false;
    this.isShown_author = false;
    this.btn_new_author = false;
    this.publisher = { id: 0, name: ''}
    this.showCreateBtn = true;
  }

  newAuthor(): void {
    this.isShown_publisher = false;
    this.isShown_category = false;
    this.isShown_image = false;
    this.isShown_author_form = true;
    this.btn_new_author = false;
    this.author_dropdown = false;
    this.btn_new_publisher = false;
    this.author = { id: 0, firstName: '', middleName: '', lastName: ''}
    this.showCreateBtn = true;
  }

  ContinuePublisherForm(): void {
    if (this.book.authorId) {
      this.btn_new_author = false;
      this.isShown_author = true;
      this.isShown_publisher = false;
      this.isShown_category= true;
      this.isShown_image = true;
      this.publisher_dropdown = true;
      this.publisherId_value = true;
      this.isShown_publisher_form = false;
      this.btn_new_publisher = false;
      this.showCreateBtn = false;
      this.disable_publisher = true;
    }
    else if (this.author.firstName && this.author.lastName) {
        this.btn_new_publisher = false;
        this.isShown_publisher = false;
        this.isShown_category = true;
        this.isShown_image = true;
        this.publisher_dropdown = true;
        this.publisherId_value = true;
        this.isShown_publisher_form = false;
        this.showCreateBtn = false;
        this.disable_publisher = true;
      }
      else {
        this.btn_new_author = true;
        this.isShown_author = true;
        this.btn_new_publisher = false;
        this.isShown_publisher = false;
        this.isShown_category = true;
        this.isShown_image = true;
        this.publisher_dropdown = true;
        this.publisherId_value = true;
        this.isShown_publisher_form = false;
        this.showCreateBtn = false;
        this.disable_publisher = true;
      }
  }

  ContinueAuthorForm(): void {
    if (this.book.publisherId) {
      this.btn_new_publisher = true;
      this.isShown_author = false;
      this.isShown_publisher = true;
      this.isShown_category= true;
      this.isShown_image = true;
      this.author_dropdown = true;
      this.authorId_value = true;
      this.isShown_author_form = false;
      this.btn_new_author = false;
      this.showCreateBtn = false;
      this.disable_author = true;
    }
    else if (this.publisher.name) {
        this.btn_new_author = false;
        this.isShown_author = false;
        this.isShown_category = true;
        this.isShown_image = true;
        this.author_dropdown = true;
        this.authorId_value = true;
        this.isShown_author_form = false;
        this.showCreateBtn = false;
        this.disable_author = true;
      }
      else {
        this.btn_new_publisher = true;
        this.isShown_publisher = true;
        this.btn_new_author = false;
        this.isShown_author = false;
        this.isShown_category = true;
        this.isShown_image = true;
        this.author_dropdown = true;
        this.authorId_value = true;
        this.isShown_author_form = false;
        this.showCreateBtn = false;
        this.disable_author = true;
      }
  }

  cancel_new_publisher(): void {
    this.isShown_publisher = true;
    this.isShown_publisher_form = false;
    this.isShown_author = true;
    this.isShown_category= true;
    this.isShown_image = true;
    this.btn_new_publisher = true;
    this.publisher_dropdown = true;
    this.btn_new_author = true;
    this.publisher = { id: 0, name: ''}
    this.showCreateBtn = false;
  }

  cancel_new_author(): void {
    this.isShown_author_form = false;
    this.isShown_author = true;
    this.isShown_publisher = true;
    this.isShown_category = true;
    this.isShown_image= true;
    this.btn_new_author = true;
    this.author_dropdown = true;
    this.btn_new_publisher = true;
    this.author = { id: 0, firstName: '', middleName: '', lastName: ''}
    this.showCreateBtn = false;
  }

  cancel(): void {
    this.book = { id: 0, title: '', language: '', description: '', publishYear: 0, categoryId: 0, authorId: 0, publisherId: 0, image: '', category: [], publisher: { id: 0, name: ''}, author: { id: 0, firstName: '', lastName: ''} };
    this.author = { id: 0, firstName: '', middleName: '', lastName: '' };
    this.publisher = { id: 0, name: ''}
    this.authorId_value = false;
    this.isShown_author = true;
    this.isShown_author_form = false;
    this.btn_new_author = true;
    this.isShown_publisher = true;
    this.isShown_category = true;
    this.isShown_image = true;
    this.author_dropdown = true;
    this.publisherId_value = false;
    this.isShown_publisher = true;
    this.isShown_publisher_form = false;
    this.btn_new_publisher = true;
    this.publisher_dropdown = true;
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
    // INSERT AUTHOR
    if(this.author.id == 0) {
      this.authorService.addAuthor(this.author)
      .subscribe({
        next: (x) => {
          this.book.authorId = x.id;
          this.authors.push(x);
          this.author = { id: 0, firstName: '', middleName: '', lastName: '' };
          this.message = '';
          console.log('Author added successfully');

          // INSERT PUBLISHER
          if(this.publisher.id == 0) {
            this.publisherService.addPublisher(this.publisher)
            .subscribe({
              next: (p) => {
                this.book.publisherId = p.id;
                this.publishers.push(p);
                this.publisher = { id: 0, name: '' };
                this.message = '';
                console.log('Publisher added successfully');

                  // INSERT BOOK
                  if(this.book.id == 0) {
                    this.bookService.addBook(this.book)
                    .subscribe({
                      next: (x) => {
                        this.books.push(x);
                        this.book = { id: 0, title: '', language: '', description: '', publishYear: 0, categoryId: 0, authorId: 0, publisherId: 0, image: '', category: [], publisher: { id: 0, name: ''}, author: { id: 0, firstName: '', lastName: ''} };
                        this.message = '';
                        Swal.fire({
                          title: 'Success!',
                          text: 'Book added successfully',
                          icon: 'success',
                          confirmButtonText: 'Continue'
                        });
                        this.authorId_value = false;
                        this.isShown_author_form = false;
                        this.btn_new_author = true;
                        this.isShown_author = true;
                      },
                      error: (err) => {
                        console.log(err.error);
                        this.message = Object.values(err.error.errors).join(", ");
                      }
                    });
                  }
                },
                  error: (err) => {
                  console.log(err.error);
                  this.message = Object.values(err.error.errors).join(", ");
                  }
              });
            }
          },
          error: (err) => {
            console.log(err.error);
            this.message = Object.values(err.error.errors).join(", ");
          }
      });
    }
  }
}
