import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/user';
import { CrudService } from './crud.service';
import { Endpoints } from '../models/endpoints';

@Injectable({
  providedIn: 'root',
})
export class UserService extends CrudService<User> {
  constructor(http: HttpClient) {
    super(http, Endpoints.Users);
  }
}
