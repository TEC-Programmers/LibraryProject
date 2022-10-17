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
import { merge } from 'rxjs';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-admin-book',
  templateUrl: './admin-book.component.html',
  styleUrls: ['./admin-book.component.css']
})
export class AdminBookComponent implements OnInit {
  public _author: Author = { id: 0, firstName: '', middleName: '', lastName: ''}
  authors: Author[] = [];
  author: Author = { id: 0, firstName: '', middleName: '', lastName: ''}
  publishers: Publisher[] = [];
  publisher: Publisher = { id: 0, name: ''}
  public _publisher: Publisher = { id: 0, name: ''}
  books: Book[] = [];
  book: Book = { id: 0, title: '', language: '', description: '', publishYear: 0, categoryId: 0, authorId: 0, publisherId: 0, image: '', category: []};
  categorys: Category[] = [];
  _category: Category = { id: 0, categoryName: ''}
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
    {"name": "Tor_Fanger_Tyve.jpg"}, 
    {"name": "Aadhi-Raat-Ka-Shehar.jpg"},
    {"name": "Hemu.jpg"},
]

  imagePreviewSrc: string | ArrayBuffer | null | undefined = '';
  isImageSelected: boolean = false;

  obj1 = {"fparams":{"keys":["a","b"],"pairs":{"p":"qwert"}},"qparams":{"x":"xyz"}}
  obj2 = {"fparams":{"keys":["c","d"],"pairs":{"q":"yuiop"}},"qparams":{"z":"zyx"}}
  file;
  public getImageName: string = '';

  constructor(private userService: UserService, private authService: AuthService, private httpClient: HttpClient, private bookService: BookService, private authorService: AuthorService, private publisherService: PublisherService, private categoryService: CategoryService) { }

  ngOnInit(): void {
    setTimeout(() => {
      this.showOrhideAdminBtn();
    });

    this.bookService.getAllBooks().subscribe(x => this.books = x);
    this.authorService.getAllAuthors().subscribe(a => this.authors = a);
    this.publisherService.getAllPublishers().subscribe(p => this.publishers = p);
    this.categoryService.getAllCategories().subscribe(c => this.categorys = c);
  }

  ngAfterViewInit() {
    setTimeout(() => {
      this.showOrhideAdminBtn();
    });
  }

  showPreview(event: Event) {
    let selectedFile = (event.target as HTMLInputElement).files?.item(0)

    if (selectedFile) {
      if (["image/jpeg", "image/png", "image/svg+xml"].includes(selectedFile.type)) {
        let fileReader = new FileReader();
        fileReader.readAsDataURL(selectedFile);

        fileReader.addEventListener('load', (event) => {
          this.imagePreviewSrc = event.target?.result;
          this.isImageSelected = true
        })
      }
    } else {
      this.isImageSelected = false
    }
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
      this.btn_new_author = true;
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
        this.publisherId_value = true;
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
    this.book = { id: 0, title: '', language: '', description: '', publishYear: 0, categoryId: 0, authorId: 0, publisherId: 0, image: '', category: []};
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
    this.isImageSelected = false;
  }

 

  edit_book(book: Book): void {
    this.message = '';
    // this.book.image = this.book.image + '.jpg';
    this.book = book;
    this.book.id = book.id || 0;
    
    console.log(this.book);
    console.log('image format: ',this.book.image) 
  }

  delete_book(book: Book): void {
    if(confirm('Er du sikker på du vil slette?')) {
      this.bookService.deleteBook(book.id)
        .subscribe(() => {
            this.books = this.books.filter(x => x.id != book.id);
        });
    }
  }

  update_Book(): void {
    console.log(this.book)
    this.message = '';

    // check if book exsist's
    if(this.book.id != 0) {
      this.bookService.updateBook(this.book.id, this.book)
      .subscribe({
        error: (err) => {
          console.log(err.error);
          this.message = Object.values(err.error.errors).join(", ");
        },
        complete: () => {
          this.message = '';
          this.book = { id: 0, title: '', language: '', description: '', publishYear: 0, categoryId: 0, authorId: 0, publisherId: 0, image: '', category: []};      
          Swal.fire({
            title: 'Success!',
            text: 'Book updated successfully',
            icon: 'success',
            confirmButtonText: 'Continue'
          });        
        }
      });
    }
    else {
      console.log("Didn't Find A Book To Update!")
    }
  }


  save_book(): void {
    this.book.image = this.getImageName;

    // Pick author and publisher from dropdown
    if (this.book.authorId > 0 && this.book.publisherId > 0) {

    // GET Author
    this.authorService.getAuthorById(this.book.authorId)
    .subscribe({
      next: (get_author) => {    
        this._author = get_author;
        this.book.authorId = this._author.id;
        console.log('auhtor: ',this._author)
        this.authors.push(this._author);
        this._author = { id: 0, firstName: '', middleName: '', lastName: '' };
          
          // GET Publisher
          this.publisherService.getPublisherById(this.book.publisherId)
          .subscribe({
            next: (get_publisher) => {
              this._publisher = get_publisher;
              this.book.publisherId = this._publisher.id;
              
              console.log('publisher: ',this._publisher)
              this.publishers.push(this._publisher);
              this._publisher = { id: 0, name: '' };

              this.categoryService.getCategoryById(this.book.categoryId)
              .subscribe({
                next: (get_category) => {
                  this._category = get_category;
                  this.book.category.push(this._category);
                  this._category = { id: 0, categoryName: '' };
                  this.book.categoryId = get_category.id;

                // INSERT BOOK
                if(this.book.id == 0) {
                  // this.book.categoryId = this.book.categoryId;
                  console.log('Book Before Add: ',this.book)
                  this.bookService.addBook(this.book)
                  .subscribe({
                    next: (x) => {
                      this.books.push(x);
                      this.book = { id: 0, title: '', language: '', description: '', publishYear: 0, categoryId: 0, authorId: 0, publisherId: 0, image: '', category: []};
                      this.message = '';
                      this.imagePreviewSrc = '';
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

                }
              })
                                                           
            }
          })                
      }
    });
    }
    else { // create new author and publisher
      console.log('author: ',this.author)
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
                    // this.book.image = this.getImageName;
                    // console.log('book: ',this.book)
                    this.bookService.addBook(this.book)
                    .subscribe({
                      next: (x) => {
                        this.books.push(x);
                        this.book = { id: 0, title: '', language: '', description: '', publishYear: 0, categoryId: 0, authorId: 0, publisherId: 0, image: '', category: []};
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
    this.isImageSelected = false;
  } 

  getImageDetails(event) {
    // loop through input image
      for (var i = 0; i < event.target.files.length; i++) { 
        // save image-name from input
        this.getImageName = event.target.files[i].name;
      
        // var type = event.target.files[i].type;
        // var size = event.target.files[i].size;
        // var modifiedDate = event.target.files[i].lastModifiedDate;
        
        // console.log ('Name: ' + name + "\n" + 
        //   'Type: ' + type + "\n" +
        //   'Last-Modified-Date: ' + modifiedDate + "\n" +
        //   'Size: ' + Math.round(size / 1024) + " KB");
      }   
  }

  
}



   