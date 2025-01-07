import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { UsersTableComponent } from './components/users-table/users-table.component';
import { GenreListComponent } from './components/genre-list/genre-list.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, UsersTableComponent, GenreListComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'h3project-angular';
}
