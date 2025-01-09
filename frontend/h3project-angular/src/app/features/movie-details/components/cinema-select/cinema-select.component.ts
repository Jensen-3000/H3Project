import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { Cinema } from '../../../../models/cinema';
import { CinemaService } from '../../../../services/cinema.service';

@Component({
  selector: 'app-cinema-select',
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatInputModule,
  ],
  templateUrl: './cinema-select.component.html',
  styleUrl: './cinema-select.component.css',
})
export class CinemaSelectComponent implements OnInit {
  @Input() movieId!: number;
  @Output() cinemaSelected = new EventEmitter<number>();
  @Output() dateSelected = new EventEmitter<Date>();

  cinemas: Cinema[] = [];
  selectedCinema?: number;
  selectedDate?: Date;
  minDate = new Date();
  maxDate = new Date(new Date().setDate(new Date().getDate() + 7));

  constructor(private cinemaService: CinemaService) {}

  ngOnInit() {
    this.loadCinemas();
  }

  loadCinemas() {
    this.cinemaService
      .getCinemasByMovie(this.movieId)
      .subscribe((cinemas) => (this.cinemas = cinemas));
  }

  onCinemaChange() {
    if (this.selectedCinema) {
      this.cinemaSelected.emit(this.selectedCinema);
    }
  }

  onDateChange() {
    if (this.selectedDate) {
      this.dateSelected.emit(this.selectedDate);
    }
  }
}
