import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { MovieService } from '../../services/movie.service';
import { Movie } from '../../models/movie';
import { MovieInfoComponent } from './components/movie-info/movie-info.component';
import { CinemaSelectComponent } from './components/cinema-select/cinema-select.component';
import { ShowtimesComponent } from './components/showtimes/showtimes.component';
import { SeatSelectionComponent } from './components/seat-selection/seat-selection.component';
import { Router } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-movie-details',
  standalone: true,
  imports: [
    CommonModule,
    MovieInfoComponent,
    CinemaSelectComponent,
    ShowtimesComponent,
    SeatSelectionComponent,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css'],
})
export class MovieDetailsComponent implements OnInit {
  movie: Movie | null = null;
  selectedCinemaId: number = 0;
  selectedShowtimeId: number = 0;
  selectedDate?: Date;
  error: string = '';

  constructor(
    private route: ActivatedRoute,
    private movieService: MovieService,
    private router: Router,
  ) {}

  ngOnInit() {
    this.route.params.subscribe((params) => {
      const slug = params['slug'];
      this.loadMovie(slug);
    });
  }

  private loadMovie(slug: string) {
    this.movieService.getMovieBySlug(slug).subscribe({
      next: (movie) => (this.movie = movie),
      error: () => this.router.navigate(['/not-found']),
    });
  }

  onCinemaSelected(cinemaId: number) {
    this.selectedCinemaId = cinemaId;
    this.selectedShowtimeId = 0;
  }

  onDateSelected(date: Date) {
    this.selectedDate = date;
    this.selectedShowtimeId = 0;
  }

  onShowtimeSelected(showtimeId: number): void {
    this.selectedShowtimeId = showtimeId;
    console.log('Selected showtime:', showtimeId);
  }
}
