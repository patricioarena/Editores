import { Component, OnInit } from '@angular/core';
import { EscritoTexto } from '../modelos/EscritoTexto';
import { EditorService } from '../service/editor.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { NotificationService } from '../service/notification.service';
import { TitleService } from '../service/title.service';

@Component({
  selector: 'app-editor-ngx-editor',
  templateUrl: './editor-ngx-editor.component.html',
  styleUrls: ['./editor-ngx-editor.component.scss']
})
export class EditorNgxEditorComponent implements OnInit {
  // Editor
  editorConfig = {
    editable: true,
    spellcheck: true,
    height: '30rem',
    minHeight: '5rem',
    placeholder: 'Enter text here... ヽ(^。^)丿',
    translate: 'no',
    toolbar: [
      ["bold", "italic", "underline", "strikeThrough", "superscript", "subscript"],
      ["fontName", "fontSize", "color"],
      ["justifyLeft", "justifyCenter", "justifyRight", "justifyFull", "indent", "outdent"],
      ["cut", "copy", "delete", "removeFormat", "undo", "redo"],
      ["paragraph", "blockquote", "removeBlockquote", "horizontalLine", "orderedList", "unorderedList"],
      ["link", "unlink"]
    ]
  };


  // Editor

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


}
