import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { TitleService } from '../service/title.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit {

  title: String;
  isEnabled = false;

  constructor(
    public spinner: NgxSpinnerService,
    private titleServive: TitleService
  ) { }

  ngOnInit() {
    this.title = this.titleServive.APP_TITLE;
  }




}

