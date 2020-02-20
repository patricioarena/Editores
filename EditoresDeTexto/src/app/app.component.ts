import { Component } from '@angular/core';
import { TitleService } from './service/title.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  public constructor(private titleService: TitleService ) {
    this.titleService.init('Editor');
  }

}

