import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorizedCheckoutFormComponent } from './authorized-checkout-form.component';

describe('AuthorizedCheckoutFormComponent', () => {
  let component: AuthorizedCheckoutFormComponent;
  let fixture: ComponentFixture<AuthorizedCheckoutFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AuthorizedCheckoutFormComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AuthorizedCheckoutFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
