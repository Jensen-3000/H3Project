import { ComponentFixture, TestBed } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SeatSelectionComponent } from './seat-selection.component';
import { SeatService } from '../../../../services/seat.service';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';
import { of } from 'rxjs';
import { Seat, SeatStatus } from '../../../../models/seat';

describe('SeatSelectionComponent', () => {
  let component: SeatSelectionComponent;
  let fixture: ComponentFixture<SeatSelectionComponent>;
  let seatServiceSpy = jasmine.createSpyObj('SeatService', ['getTheaterLayout', 'reserveSeats']);

  beforeEach(async () => {
    seatServiceSpy.getTheaterLayout.and.returnValue(
      of({
        rows: 5,
        seatsPerRow: 8,
        seats: [{ id: 1, row: 1, number: 1, status: SeatStatus.Available, price: 10 }],
      }),
    );

    await TestBed.configureTestingModule({
      imports: [
        BrowserAnimationsModule,
        MatSelectModule,
        MatFormFieldModule,
        FormsModule,
        SeatSelectionComponent,
      ],
      providers: [{ provide: SeatService, useValue: seatServiceSpy }],
    }).compileComponents();

    fixture = TestBed.createComponent(SeatSelectionComponent);
    component = fixture.componentInstance;
    component.showtimeId = 1;
    fixture.detectChanges();
  });

  it('should load theater layout on init', () => {
    expect(seatServiceSpy.getTheaterLayout).toHaveBeenCalledWith(1);
  });

  it('should toggle seat selection', () => {
    const seat = { id: 1, row: 1, number: 1, status: SeatStatus.Available, price: 10 };
    component.toggleSeat(seat);
    expect(component.selectedSeats.length).toBe(1);
    component.toggleSeat(seat);
    expect(component.selectedSeats.length).toBe(0);
  });
});
