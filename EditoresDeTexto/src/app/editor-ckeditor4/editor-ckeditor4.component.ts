import { Component, OnInit, ViewChild } from '@angular/core';
import { EscritoTexto } from '../modelos/EscritoTexto';
import { EditorService } from '../service/editor.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { NotificationService } from '../service/notification.service';
import { TitleService } from '../service/title.service';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-editor-ckeditor4',
  templateUrl: './editor-ckeditor4.component.html',
  styleUrls: ['./editor-ckeditor4.component.scss']
})
export class EditorCKEditorComponent implements OnInit {

  // Editor
  // https://ckeditor.com/docs/ckeditor4/latest/features/toolbar.html
  // https://ckeditor.com/latest/samples/toolbarconfigurator/index.html#advanced
  // https://github.com/ckeditor/ckeditor4-angular/issues/33
  // https://ckeditor.com/docs/ckeditor4/latest/features/styles.html#widget-styles
  // https://ckeditor.com/cke4/addons/skins/all
  // https://openbase.io/js/ngx-highlight-js
  // Editor

  title: String;
  escritoTexto_Titulo: String;
  escritoTexto_Texto: String;
  textPreview: String = '';
  escritoTexto: EscritoTexto;
  isEnabled = false;
  showPreview = false;
  showExample = false;
  fileUrl;
  response;
  baseUrl = environment.BASE_URL;

  example = `<h1><img alt="Saturn V carrying Apollo 11" class="right" src="assets/images/examples/apolo_11.jpg" /> Apollo 11</h1>`+
  `<p><strong>Apollo 11</strong> was the spaceflight that landed the first humans, Americans <a href="http://en.wikipedia.org/wiki/Neil_Armstrong">Neil Armstrong</a> and <a href="http://en.wikipedia.org/wiki/Buzz_Aldrin">Buzz Aldrin</a>, on the Moon on July 20, 1969, at 20:18 UTC. Armstrong became the first to step onto the lunar surface 6 hours later on July 21 at 02:56 UTC.</p>`+
  `<p>Armstrong spent about <s><span style="background-color:#f1c40f">three and a half</span></s> two and a half hours outside the spacecraft, Aldrin slightly less; and together they collected 47.5 pounds (21.5&nbsp;kg) of lunar material for return to Earth. A third member of the mission, <a href="http://en.wikipedia.org/wiki/Michael_Collins_(astronaut)">Michael Collins</a>, piloted the <a href="http://en.wikipedia.org/wiki/Apollo_Command/Service_Module">command</a> spacecraft alone in lunar orbit until Armstrong and Aldrin returned to it for the trip back to Earth.</p>`+
  `<h2>Broadcasting and <em>quotes</em> <a id="quotes" name="quotes"></a></h2>`+
  `<p>Broadcast on live TV to a world-wide audience, Armstrong stepped onto the lunar surface and described the event as:</p>`+
  `<blockquote>`+
  `<p>One small step for [a] man, one giant leap for mankind.</p>`+
  `</blockquote>`+
  `<p>Apollo 11 effectively ended the <a href="http://en.wikipedia.org/wiki/Space_Race">Space Race</a> and fulfilled a national goal proposed in 1961 by the late U.S. President <a href="http://en.wikipedia.org/wiki/John_F._Kennedy">John F. Kennedy</a> in a speech before the United States Congress:</p>`+
  `<blockquote>`+
  `<p>[...] before this decade is out, of landing a man on the Moon and returning him safely to the Earth.</p>`+
  `</blockquote>`+
  `<p><small>Source: <a href="http://en.wikipedia.org/wiki/Apollo_11">Wikipedia.org</a>&nbsp;<sub><img alt="smiley" src="${this.baseUrl}assets/ckeditor/plugins/smiley/images/regular_smile.png" style="height:23px; width:23px" title="smiley" /></sub></small></p>`+
  `<hr />`+
  `<h2 style="text-align:center">The Flavorful Tuscany Meetup</h2>`+
  `<p style="text-align:center"><strong>Welcome letter</strong></p>`+
  `<p>Dear Guest,</p>`+
  `<p>We are delighted to welcome you to the annual <em>Flavorful Tuscany Meetup</em> and hope you will enjoy the programme as well as your stay at the Bilancino Hotel.</p>`+
  `<p>Please find below the full schedule of the event.</p>`+
  `<table cellpadding="15" cellspacing="0">`+
  `  <thead>`+
  `    <tr>`+
  `      <th colspan="2" scope="col">Saturday, July 14</th>`+
  `    </tr>`+
  `  </thead>`+
  `  <tbody>`+
  `    <tr>`+
  `      <td>9:30 AM - 11:30 AM</td>`+
  `      <td>Americano vs. Brewed - &ldquo;know your coffee&rdquo; session with <strong>Stefano Garau</strong></td>`+
  `    </tr>`+
  `    <tr>`+
  `      <td>1:00 PM - 3:00 PM</td>`+
  `      <td>Pappardelle al pomodoro - live cooking session with <strong>Rita Fresco</strong></td>`+
  `    </tr>`+
  `    <tr>`+
  `      <td>5:00 PM - 8:00 PM</td>`+
  `      <td>Tuscan vineyards at a glance - wine-tasting session with <strong>Frederico Riscoli</strong></td>`+
  `    </tr>`+
  `  </tbody>`+
  `</table>`+
  `<blockquote>`+
  `<p>The annual Flavorful Tuscany meetups are always a culinary discovery. You get the best of Tuscan flavors during an intense one-day stay at one of the top hotels of the region. All the sessions are lead by top chefs passionate about their profession. I would certainly recommend to save the date in your calendar for this one!</p>`+
  `<p>Angelina Calvino, food journalist</p>`+
  `</blockquote>`+
  `<p>Please arrive at the Bilancino Hotel reception desk at least <strong>half an hour earlier</strong> to make sure that the registration process goes as smoothly as possible.</p>`+
  `<p>We look forward to welcoming you to the event.</p>`+
  `<p><strong>Victoria Valc</strong><br />`+
  `<strong>Event Manager</strong><br />`+
  `<strong>Bilancino Hotel</strong></p>`;

  constructor(
    public editorService: EditorService,
    public spinner: NgxSpinnerService,
    private notificationService: NotificationService,
    private titleServive: TitleService
  ) { }

  ngOnInit() {
    this.title = this.titleServive.APP_TITLE;
    this.escritoTexto_Titulo = '';
    this.escritoTexto_Texto = '';
  }

  Guardar() {
    this.isEnabled = false;
    this.showPreview = false;
    this.textPreview = '';
    // console.log("TextArea::text: " + this.text);
    this.escritoTexto = new EscritoTexto;
    this.escritoTexto.Titulo = this.escritoTexto_Titulo;

    this.escritoTexto.Texto = this.escritoTexto_Texto;


    // this.objeto.Extension = '.xml';
    this.editorService.nuevoEscritoTexto(this.escritoTexto).subscribe(
      resp => {
        // if (resp === '-1') {
        //   // this.notificationService.show('Certificado', 'Certificado no valido', 'info');
        // } else if (resp === '-2') {
        //   this.notificationService.showInfo('Certificado', 'Certificado no valido');
        // } else {
        //   this.response = resp;
        //   this.isEnabled = true;
        // }
      }, err => {
        // console.log(JSON.parse(err.error).ExceptionMessage)
        // const message = JSON.parse(err.error).ExceptionMessage;
        // this.notificationService.showError('Error', message);
      });
  }

  preview() {
    this.showPreview = this.showPreview ? false : true;
    this.textPreview = this.escritoTexto_Texto;
    console.log(this.escritoTexto_Texto);
  }

  loadExample(){
    this.escritoTexto_Texto = this.example;
    this.escritoTexto_Titulo = 'Multi Example';
    this.showExample = true;
  }

  unLoadExample(){
    this.escritoTexto_Texto = '';
    this.escritoTexto_Titulo = '';
    this.showExample = false;
  }

}
