import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BookService } from '../_services/book.service';
import { CategoryService } from '../_services/category.service';
import { DarkModeService } from 'angular-dark-mode';
import { Observable } from 'rxjs';


@Component({
  selector: 'app-loan',
  templateUrl: './loan.component.html',

  styleUrls: ['./loan.component.css']
})
export class LoanComponent implements OnInit {
  dateRangeForm!: FormGroup;
  darkMode$: Observable<boolean> = this.darkModeService.darkMode$;

  constructor(private bookService: BookService, private categoryService: CategoryService,  private formBuilder: FormBuilder, private darkModeService: DarkModeService) {}
  range = new FormGroup({
    fromDate: new FormControl('', Validators.required),
    toDate: new FormControl('', Validators.required)
  });

  ngOnInit(): void {
    this.dateRangeForm = this.formBuilder.group({
      fromDate: new FormControl('', Validators.required),
      toDate: new FormControl('', Validators.required)
    });

  }
  onToggle(): void {
    this.darkModeService.toggle();
  }


  onFormSubmit() {
    console.log('Is Form Invalid', this.dateRangeForm.invalid);
  }


}
