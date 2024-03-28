import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ControlsFilterBarComponent } from './controls-filter-bar.component';

describe('ControlsFilterBarComponent', () => {
  let component: ControlsFilterBarComponent;
  let fixture: ComponentFixture<ControlsFilterBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ControlsFilterBarComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ControlsFilterBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
