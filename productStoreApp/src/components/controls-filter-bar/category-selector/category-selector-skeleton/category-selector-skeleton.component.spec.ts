import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CategorySelectorSkeletonComponent } from './category-selector-skeleton.component';

describe('CategorySelectorSkeletonComponent', () => {
  let component: CategorySelectorSkeletonComponent;
  let fixture: ComponentFixture<CategorySelectorSkeletonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CategorySelectorSkeletonComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(CategorySelectorSkeletonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
