import { ViewportScroller } from '@angular/common';
import { Component, ElementRef, HostListener, ViewChild } from '@angular/core';
import { CategoryComponent } from './category/category.component';
import { Category } from './_models/Category';
import { CategoryService } from './_services/category.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'LibraryProject-Client';
//   pageYoffset = 0;
//   @HostListener('window:scroll', ['$event']) onScroll(event){
//     this.pageYoffset = window.pageYOffset;
//  }
  @ViewChild('scroll')
  scroll!: ElementRef;

  categories: Category[] = [];


  constructor(private categoryService:CategoryService, /*private scroll:ViewportScroller*/) {}

  ngOnInit(): void {
    this.categoryService.getAllCategories()
    .subscribe(c => this.categories = c);

  }

 scrollToTop(){
  // this.scroll.scrollToPosition([0,0]);
  this.scroll.nativeElement.scrollToTop = 0;
}

scrollToBottom(){
  console.log(this.scroll.nativeElement.scrollHeight)
  this.scroll.nativeElement.scrollToTop = this.scroll.nativeElement.scrollHeight;
}

}
