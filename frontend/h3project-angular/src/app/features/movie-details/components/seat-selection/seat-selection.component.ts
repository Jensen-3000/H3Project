import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { FormsModule } from '@angular/forms';
import { SeatService } from '../../../../services/seat.service';
import { Seat, SeatStatus, TheaterLayout, SeatGroup } from '../../../../models/seat';

@Component({
  selector: 'app-seat-selection',
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatSelectModule, MatProgressSpinnerModule, FormsModule],
  templateUrl: './seat-selection.component.html',
  styleUrls: ['./seat-selection.component.css'],
})
export class SeatSelectionComponent implements OnInit {
  @Input() showtimeId!: number;

  theaterLayout?: TheaterLayout;
  selectedSeats: Seat[] = [];
  ticketQuantity = 1;
  readonly maxTickets = 10;
  SeatStatus = SeatStatus;
  hoveredSeat: Seat | null = null;
  groupedSeats: SeatGroup[] = [];

  constructor(private seatService: SeatService) {}

  ngOnInit() {
    this.loadTheaterLayout();
  }

  loadTheaterLayout() {
    this.seatService.getTheaterLayout(this.showtimeId).subscribe((layout: TheaterLayout) => {
      this.theaterLayout = layout;
      this.groupSeats();
    });
  }

  private groupSeats() {
    if (!this.theaterLayout?.seats) {
      this.groupedSeats = [];
      return;
    }

    const groups: { [key: string]: SeatGroup } = {};
    this.theaterLayout.seats.forEach((seat) => {
      if (!groups[seat.row]) {
        groups[seat.row] = { row: seat.row, seats: [] };
      }
      groups[seat.row].seats.push(seat);
    });

    this.groupedSeats = Object.values(groups).sort((a, b) => a.row.localeCompare(b.row));
  }

  getGroupedSeats(): SeatGroup[] {
    return this.groupedSeats;
  }

  trackByRow(index: number, group: SeatGroup): string {
    return group.row;
  }

  trackBySeat(index: number, seat: Seat): number {
    return seat.id;
  }

  toggleSeat(seat: Seat) {
    if (seat.status !== SeatStatus.Available && !this.isSeatSelected(seat)) {
      return;
    }

    if (this.isSeatSelected(seat)) {
      this.selectedSeats = this.selectedSeats.filter((s) => s.id !== seat.id);
      return;
    }

    if (this.selectedSeats.length >= this.ticketQuantity) {
      return;
    }

    this.selectedSeats = [...this.selectedSeats, seat];
  }

  isSeatSelected(seat: Seat): boolean {
    return this.selectedSeats.some((s) => s.id === seat.id);
  }

  getTotalPrice(): number {
    return this.selectedSeats.reduce((total, seat) => total + seat.price, 0);
  }

  confirmSelection() {
    if (this.selectedSeats.length === this.ticketQuantity) {
      const seatIds = this.selectedSeats.map((seat) => seat.id);
      this.seatService.reserveSeats(this.showtimeId, seatIds).subscribe({
        next: () => {
          this.selectedSeats = [];
          this.loadTheaterLayout();
        },
        error: (error: any) => {
          console.error('Failed to reserve seats:', error);
        },
      });
    }
  }
}
