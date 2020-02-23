import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from '../home/home.component';
import { EditorNgxEditorComponent } from '../editor-ngx-editor/editor-ngx-editor.component';
import { EditorNgxQuillComponent } from '../editor-ngx-quill/editor-ngx-quill.component';


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
    path: 'ngx-editor',
    component: EditorNgxEditorComponent,
    data: {title: 'Ngx-Editor'}
  },
  {
    path: 'quill',
    component: EditorNgxQuillComponent,
    data: {title: 'Quill'}
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class RoutingModule {

}
