import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditorNgxQuillComponent } from './editor-ngx-quill.component';

describe('EditorNgxQuillComponent', () => {
  let component: EditorNgxQuillComponent;
  let fixture: ComponentFixture<EditorNgxQuillComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditorNgxQuillComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditorNgxQuillComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
