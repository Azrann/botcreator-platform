import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RemoveBotComponent } from './remove-bot.component';

describe('RemoveBotComponent', () => {
  let component: RemoveBotComponent;
  let fixture: ComponentFixture<RemoveBotComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RemoveBotComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RemoveBotComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
