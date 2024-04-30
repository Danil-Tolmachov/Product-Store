import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderBriefComponent } from './order-brief.component';

describe('OrderBriefComponent', () => {
  let component: OrderBriefComponent;
  let fixture: ComponentFixture<OrderBriefComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OrderBriefComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(OrderBriefComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
