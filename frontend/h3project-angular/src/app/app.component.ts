import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { UsersTableComponent } from './components/users-table/users-table.component';
import { GenreListComponent } from './components/genre-list/genre-list.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, UsersTableComponent, GenreListComponent, NavbarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  constructor(public authService: AuthService) {}
  title = 'h3project-angular';
}
