import { BrowserModule, Title } from '@angular/platform-browser';
import { NgModule, Component } from '@angular/core';

import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ModalModule } from 'ngx-bootstrap/modal';

import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { HomeComponent } from './home/home.component';

import { FormsModule } from '@angular/forms';
import { FileUploadModule } from 'ng2-file-upload';

import { EditorService } from './service/editor.service';
import { HttpClientModule } from '@angular/common/http';

import { NgxSpinnerModule } from 'ngx-spinner';
import { NgxSpinnerService } from 'ngx-spinner';
import { NgxEditorModule } from 'ngx-editor';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { NotificationService } from './service/notification.service';
import { RoutingModule } from './modules/routing.module';
import { TitleService } from './service/title.service';
import { FooterComponent } from './footer/footer.component';
import { HeaderComponent } from './header/header.component';
import { ClipboardModule } from 'ngx-clipboard';
import { ModalComponent } from './modal/modal.component';
import { EditorNgxEditorComponent } from './editor-ngx-editor/editor-ngx-editor.component';
import { EditorNgxQuillComponent } from './editor-ngx-quill/editor-ngx-quill.component';
import { EditorCKEditorComponent } from './editor-ckeditor4/editor-ckeditor4.component';
import { QuillModule } from 'ngx-quill';
import { CKEditorModule } from 'ckeditor4-angular';


@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    FooterComponent,
    HeaderComponent,
    ModalComponent,
    EditorNgxEditorComponent,
    EditorNgxQuillComponent,
    EditorCKEditorComponent
  ],
  imports: [
    ClipboardModule,
    BrowserModule,
    HttpClientModule,
    FormsModule,
    FileUploadModule,
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot(), // ToastrModule added
    BsDropdownModule.forRoot(),
    TooltipModule.forRoot(),
    ModalModule.forRoot(),
    NgxSpinnerModule,
    NgxEditorModule ,
    TooltipModule.forRoot(),
    RoutingModule,
    QuillModule.forRoot(),
    CKEditorModule
  ],
  providers: [ EditorService, NgxSpinnerService, NotificationService, TitleService ],
  bootstrap: [AppComponent],
  entryComponents: [ ModalComponent ]
})
export class AppModule { }
