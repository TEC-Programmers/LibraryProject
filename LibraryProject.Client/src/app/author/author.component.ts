import { Component, OnInit } from '@angular/core';
import { AuthorService } from '../_services/author.service';
import { Author } from '../_models/Author';
@Component({
  selector: 'app-author',
  templateUrl: './author.component.html',
  styleUrls: ['./author.component.css']
})
export class AuthorComponent implements OnInit {

  authors: Author [] = [];

  author: Author = {id: 0, firstName: '', middleName: '',  lastName: ''};



  constructor(private authorService: AuthorService) { }

  ngOnInit(): void {
    this.authorService.getAllAuthors()
    .subscribe(p => this.authors = p);
  }

}
