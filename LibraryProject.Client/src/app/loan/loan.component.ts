import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-loan',
  templateUrl: './loan.component.html',
  styleUrls: ['./loan.component.css']
})
export class LoanComponent implements OnInit {
  birthday: Date = new Date();
  myBirthdaySettings: any = {
    theme: 'ios',
    display: 'bottom'
  };

  constructor() { }

  ngOnInit(): void {

  }


}
