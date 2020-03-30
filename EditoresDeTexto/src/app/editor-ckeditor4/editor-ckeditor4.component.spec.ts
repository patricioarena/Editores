import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditorCKEditorComponent } from './editor-ckeditor4.component';

describe('EditorNgxEditorComponent', () => {
  let component: EditorCKEditorComponent;
  let fixture: ComponentFixture<EditorCKEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditorCKEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditorCKEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
