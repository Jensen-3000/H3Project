export interface Seat {
  id: number;
  row: string;
  number: number;
  status: SeatStatus;
  price: number;
}

export interface SeatGroup {
  row: string;
  seats: Seat[];
}

export enum SeatStatus {
  Available = 'available',
  Selected = 'selected',
  Occupied = 'occupied',
  Disabled = 'disabled',
}

export interface TheaterLayout {
  rows: number;
  seatsPerRow: number;
  seats: Seat[];
}
