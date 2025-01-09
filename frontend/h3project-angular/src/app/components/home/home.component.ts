import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MovieService } from '../../services/movie.service';
import { Movie } from '../../models/movie';
import { MovieCardComponent } from '../movie-card/movie-card.component';

@Component({
  selector: 'app-home',
  imports: [CommonModule, MatCardModule, MovieCardComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent implements OnInit {
  movies: Movie[] = [];
  loading = false;
  error = '';

  constructor(private movieService: MovieService) {}

  ngOnInit() {
    this.loading = true;
    this.movieService.getAllMovies().subscribe({
      next: (movies) => {
        this.movies = movies;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load movies';
        this.loading = false;
      },
    });
  }
}
