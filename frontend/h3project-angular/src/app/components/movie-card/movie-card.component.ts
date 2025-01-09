import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Movie } from '../../models/movie';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-movie-card',
  imports: [CommonModule, MatCardModule],
  templateUrl: './movie-card.component.html',
  styleUrl: './movie-card.component.css',
})
export class MovieCardComponent {
  @Input() movie!: Movie;

  constructor(private router: Router) {}

  navigateToMovie() {
    if (this.movie && this.movie.slug) {
      this.router.navigate(['/film', this.movie.slug]);
    } else {
      console.error('Movie or slug is undefined');
    }
  }
}
