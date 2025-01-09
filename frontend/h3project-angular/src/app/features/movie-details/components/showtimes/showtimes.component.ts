import { Component, Input, OnChanges, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { Showtime } from '../../../../models/showtime';
import { ShowtimeGroup } from '../../../../models/showtime-group';
import { CinemaService } from '../../../../services/cinema.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-showtimes',
  imports: [CommonModule, MatButtonModule],
  templateUrl: './showtimes.component.html',
  styleUrl: './showtimes.component.css',
})
export class ShowtimesComponent implements OnChanges {
  @Input() movieId!: number;
  @Input() cinemaId!: number;
  @Output() showtimeSelected = new EventEmitter<number>();

  showtimeGroups: ShowtimeGroup[] = [];
  selectedShowtime: number | null = null;
  loading = false;
  error = '';

  constructor(
    private cinemaService: CinemaService,
    private router: Router,
  ) {}

  ngOnChanges(): void {
    if (this.movieId && this.cinemaId) {
      this.loadShowtimes();
    }
  }

  private loadShowtimes(): void {
    this.loading = true;
    this.error = '';
    this.selectedShowtime = null;

    this.cinemaService.getShowtimes(this.movieId, this.cinemaId).subscribe({
      next: (showtimes) => {
        const validShowtimes = showtimes.filter(
          (s) => s.showTime && !isNaN(new Date(s.showTime).getTime()),
        );
        this.showtimeGroups = this.groupShowtimes(validShowtimes);
        this.loading = false;
      },
      error: (err) => {
        console.error('Failed to load showtimes:', err);
        this.error = 'Failed to load showtimes. Please try again.';
        this.loading = false;
      },
    });
  }

  private groupShowtimes(showtimes: Showtime[]): ShowtimeGroup[] {
    const groups = new Map<string, Showtime[]>();

    showtimes.forEach((showtime) => {
      const date = new Date(showtime.showTime);
      const dateString = date.toISOString().split('T')[0]; // Get YYYY-MM-DD

      if (!groups.has(dateString)) {
        groups.set(dateString, []);
      }
      groups.get(dateString)?.push(showtime);
    });

    return Array.from(groups.entries()).map(([date, showtimes]) => ({
      date,
      showtimes: showtimes.sort(
        (a, b) => new Date(a.showTime).getTime() - new Date(b.showTime).getTime(),
      ),
    }));
  }

  selectShowtime(id: number): void {
    this.selectedShowtime = id;
    this.showtimeSelected.emit(id);
  }

  proceedToSeats(): void {
    if (this.selectedShowtime) {
      this.router.navigate(['/seats', this.selectedShowtime]);
    }
  }
}
