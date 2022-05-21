import { Component } from '@angular/core';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'LibraryProject-Client';


  public categories = ["børnebøger","voksenbøger", "Manga",]

  constructor() {}

  ngOnInit(): void {
  }

}
