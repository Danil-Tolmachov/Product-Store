import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ControlsFilterBarSkeletonComponent } from './controls-filter-bar-skeleton.component';

describe('ControlsFilterBarSkeletonComponent', () => {
  let component: ControlsFilterBarSkeletonComponent;
  let fixture: ComponentFixture<ControlsFilterBarSkeletonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ControlsFilterBarSkeletonComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ControlsFilterBarSkeletonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
