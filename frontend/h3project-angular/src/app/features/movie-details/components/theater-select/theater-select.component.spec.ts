import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TheaterSelectComponent } from './theater-select.component';

describe('TheaterSelectComponent', () => {
  let component: TheaterSelectComponent;
  let fixture: ComponentFixture<TheaterSelectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TheaterSelectComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TheaterSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
