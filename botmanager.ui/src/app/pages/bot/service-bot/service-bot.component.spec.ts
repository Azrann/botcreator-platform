import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ServiceBotComponent } from './service-bot.component';

describe('ServiceBotComponent', () => {
  let component: ServiceBotComponent;
  let fixture: ComponentFixture<ServiceBotComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ServiceBotComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ServiceBotComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
