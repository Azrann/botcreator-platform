import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BotTestComponent } from './bot-test.component';

describe('BotTestComponent', () => {
  let component: BotTestComponent;
  let fixture: ComponentFixture<BotTestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BotTestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BotTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
