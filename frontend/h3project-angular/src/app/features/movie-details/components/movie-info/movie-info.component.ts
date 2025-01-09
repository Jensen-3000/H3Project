import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { Movie } from '../../../../models/movie';

@Component({
  selector: 'app-movie-info',
  imports: [CommonModule, MatCardModule, MatChipsModule],
  templateUrl: './movie-info.component.html',
  styleUrl: './movie-info.component.css',
})
export class MovieInfoComponent {
  @Input() movie!: Movie;
}
