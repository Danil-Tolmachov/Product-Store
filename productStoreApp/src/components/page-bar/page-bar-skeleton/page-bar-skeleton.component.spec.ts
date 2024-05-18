import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PageBarSkeletonComponent } from './page-bar-skeleton.component';

describe('PageBarSkeletonComponent', () => {
  let component: PageBarSkeletonComponent;
  let fixture: ComponentFixture<PageBarSkeletonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PageBarSkeletonComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PageBarSkeletonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
