import { ComponentFixture, TestBed } from '@angular/core/testing';

import SpecificationsContainerComponent from './specifications-container.component';

describe('SpecificationsContainerComponent', () => {
  let component: SpecificationsContainerComponent;
  let fixture: ComponentFixture<SpecificationsContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SpecificationsContainerComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(SpecificationsContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
