import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditorNg2CKEditorComponent } from './editor-ng2-ckeditor.component';

describe('EditorNgxEditorComponent', () => {
  let component: EditorNg2CKEditorComponent;
  let fixture: ComponentFixture<EditorNg2CKEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditorNg2CKEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditorNg2CKEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
