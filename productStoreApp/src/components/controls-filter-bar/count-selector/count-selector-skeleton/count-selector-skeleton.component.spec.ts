import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CountSelectorSkeletonComponent } from './count-selector-skeleton.component';

describe('CountSelectorSkeletonComponent', () => {
  let component: CountSelectorSkeletonComponent;
  let fixture: ComponentFixture<CountSelectorSkeletonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CountSelectorSkeletonComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CountSelectorSkeletonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
