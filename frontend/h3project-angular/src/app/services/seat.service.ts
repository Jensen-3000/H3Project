import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CrudService } from './crud.service';
import { Endpoints } from '../models/endpoints';
import { Observable } from 'rxjs';
import { TheaterLayout, Seat } from '../models/seat';

@Injectable({
  providedIn: 'root',
})
export class SeatService extends CrudService<Seat> {
  constructor(http: HttpClient) {
    super(http, Endpoints.Seats);
  }

  getTheaterLayout(showtimeId: number): Observable<TheaterLayout> {
    return this.http.get<TheaterLayout>(
      `${this.baseUrl}/${this.endpoint}/showtime/${showtimeId}/layout`,
    );
  }

  reserveSeats(showtimeId: number, seatIds: number[]): Observable<boolean> {
    return this.http.post<boolean>(`${this.baseUrl}/${this.endpoint}/reserve`, {
      showtimeId,
      seatIds,
    });
  }
}
