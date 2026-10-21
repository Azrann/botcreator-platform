import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExtraLayoutComponent } from './extra-layout.component';

describe('ExtraLayoutComponent', () => {
  let component: ExtraLayoutComponent;
  let fixture: ComponentFixture<ExtraLayoutComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExtraLayoutComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExtraLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
