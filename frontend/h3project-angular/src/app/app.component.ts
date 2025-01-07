import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { UsersTableComponent } from './components/users-table/users-table.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, UsersTableComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'h3project-angular';
}
