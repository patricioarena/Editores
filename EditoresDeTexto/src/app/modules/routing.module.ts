import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from '../home/home.component';
import { EditorNgxQuillComponent } from '../editor-ngx-quill/editor-ngx-quill.component';
import { EditorCKEditorComponent } from '../editor-ckeditor4/editor-ckeditor4.component';


const routes: Routes = [
  {
    path: '', redirectTo: '/home',
    pathMatch: 'full'
  },
  {
    path: 'home',
    component: HomeComponent,
    data: {title: 'Home'}
  },
  {
    path: 'quill',
    component: EditorNgxQuillComponent,
    data: {title: 'Quill'}
  },
  {
    path: 'ckeditor',
    component: EditorCKEditorComponent,
    data: {title: 'CKeditor'}
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class RoutingModule {

}
