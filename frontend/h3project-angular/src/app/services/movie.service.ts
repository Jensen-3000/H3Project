import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Movie } from '../models/movie';
import { CrudService } from './crud.service';
import { Endpoints } from '../models/endpoints';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MovieService extends CrudService<Movie> {
  constructor(http: HttpClient) {
    super(http, Endpoints.Movies);
  }

  getAllMovies(): Observable<Movie[]> {
    return this.http.get<Movie[]>(`${this.baseUrl}/${this.endpoint}`);
  }

  getMovieBySlug(slug: string): Observable<Movie> {
    return this.http.get<Movie>(`${this.baseUrl}/${this.endpoint}/${slug}`);
  }
}
