import { Component } from '@angular/core';

@Component({
  selector: 'app-theater-select',
  imports: [CommonModule, MatFormFieldModule, MatSelectModule],
  templateUrl: './theater-select.component.html',
  styleUrl: './theater-select.component.css',
})
export class TheaterSelectComponent {
  @Input() theaters: any[] = [];
  @Output() theaterSelected = new EventEmitter<number>();
  selectedTheaterId?: number;

  onTheaterChange(event: any) {
    this.theaterSelected.emit(event.value);
  }
}
