import { Component, OnInit, Input } from '@angular/core';
import { EditorService } from '../service/editor.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { saveAs } from 'file-saver';
import { resolve } from 'path';
import { error } from 'util';
import { NotificationService } from '../service/notification.service';
import { TitleService } from '../service/title.service';
import { EscritoTexto } from '../modelos/EscritoTexto';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit {
  // Editores
  sin_editor = false;


  // Editores

  title: String;
  escritoTexto_Titulo: String;
  escritoTexto_Texto: String;
  textPreview: String = '';
  escritoTexto: EscritoTexto;
  isEnabled = false;
  showPreview = false;
  fileUrl;
  response;

  constructor(
    public editorService: EditorService,
    public spinner: NgxSpinnerService,
    private notificationService: NotificationService,
    private titleServive: TitleService
  ) { }

  ngOnInit() {
    this.title = this.titleServive.APP_TITLE;
    this.escritoTexto_Titulo = 'Titulo de escrito';
    this.escritoTexto_Texto = 'Texto de escrito';
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

  Sin_editor() {
    this.sin_editor = ( this.sin_editor === false ) ? true : false;
  }

}

