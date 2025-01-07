import { Component } from '@angular/core';
import { Genre } from '../../models/genre';
import { GenreService } from '../../services/genre.service';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { GenreDialogComponent } from '../genre-dialog/genre-dialog.component';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-genre-list',
  imports: [
    CommonModule,
    MatDialogModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
  ],
  templateUrl: './genre-list.component.html',
  styleUrl: './genre-list.component.css',
})
export class GenreListComponent {
  genres: Genre[] = [];
  displayedColumns: string[] = ['id', 'name', 'actions'];

  constructor(
    private genreService: GenreService,
    private dialog: MatDialog,
  ) {}

  ngOnInit(): void {
    this.loadGenres();
  }

  loadGenres(): void {
    this.genreService.getAll().subscribe((genres) => {
      this.genres = genres;
    });
  }

  openDialog(genre?: Genre): void {
    const dialogRef = this.dialog.open(GenreDialogComponent, {
      width: '250px',
      data: genre || { name: '' },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        if (genre) {
          this.genreService.update(genre.id, result).subscribe(() => {
            this.loadGenres();
          });
        } else {
          this.genreService.create(result).subscribe(() => {
            this.loadGenres();
          });
        }
      }
    });
  }

  deleteGenre(id: number): void {
    if (confirm('Are you sure?')) {
      this.genreService.delete(id).subscribe(() => {
        this.loadGenres();
      });
    }
  }
}
