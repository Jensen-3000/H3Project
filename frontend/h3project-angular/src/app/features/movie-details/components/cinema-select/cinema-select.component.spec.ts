import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CinemaSelectComponent } from './cinema-select.component';

describe('CinemaSelectComponent', () => {
  let component: CinemaSelectComponent;
  let fixture: ComponentFixture<CinemaSelectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CinemaSelectComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CinemaSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
