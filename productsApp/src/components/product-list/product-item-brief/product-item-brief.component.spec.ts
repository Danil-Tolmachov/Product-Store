import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductItemBriefComponent } from './product-item-brief.component';

describe('ProductItemBriefComponent', () => {
  let component: ProductItemBriefComponent;
  let fixture: ComponentFixture<ProductItemBriefComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProductItemBriefComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ProductItemBriefComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
