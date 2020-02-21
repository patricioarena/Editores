import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditorNgxEditorComponent } from './editor-ngx-editor.component';

describe('EditorNgxEditorComponent', () => {
  let component: EditorNgxEditorComponent;
  let fixture: ComponentFixture<EditorNgxEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditorNgxEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditorNgxEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
