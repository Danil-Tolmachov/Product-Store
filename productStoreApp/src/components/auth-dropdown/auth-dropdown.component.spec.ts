import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthDropdownComponent } from './auth-dropdown.component';

describe('AuthDropdownComponent', () => {
  let component: AuthDropdownComponent;
  let fixture: ComponentFixture<AuthDropdownComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AuthDropdownComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AuthDropdownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
