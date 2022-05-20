import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.css']
})
export class BookComponent implements OnInit {

  constructor() { }

  public books = ["Naruto","Harry Potter", "Malcolm X: Autobiography",]

  ngOnInit(): void {
  }

}
