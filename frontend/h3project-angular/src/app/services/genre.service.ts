import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Endpoints } from '../models/endpoints';
import { Genre } from '../models/genre';
import { CrudService } from './crud.service';

@Injectable({
  providedIn: 'root',
})
export class GenreService extends CrudService<Genre> {
  constructor(http: HttpClient) {
    super(http, Endpoints.Genres);
  }
}
