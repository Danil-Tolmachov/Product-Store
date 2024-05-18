import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PageBarComponent } from './page-bar.component';

describe('PageBarComponent', () => {
  let component: PageBarComponent;
  let fixture: ComponentFixture<PageBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PageBarComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PageBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
