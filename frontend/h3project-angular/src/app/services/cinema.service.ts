import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Cinema } from '../models/cinema';
import { CrudService } from './crud.service';
import { Endpoints } from '../models/endpoints';
import { Observable } from 'rxjs';
import { Showtime } from '../models/showtime';

@Injectable({
  providedIn: 'root',
})
export class CinemaService extends CrudService<Cinema> {
  constructor(http: HttpClient) {
    super(http, Endpoints.Cinemas);
  }

  getCinemasByMovie(movieId: number): Observable<Cinema[]> {
    return this.http.get<Cinema[]>(`${this.baseUrl}/${this.endpoint}/movie/${movieId}`);
  }

  getShowtimes(movieId: number, cinemaId: number): Observable<Showtime[]> {
    return this.http.get<Showtime[]>(
      `${this.baseUrl}/Schedule/movie/${movieId}/cinema/${cinemaId}`,
    );
  }
}
