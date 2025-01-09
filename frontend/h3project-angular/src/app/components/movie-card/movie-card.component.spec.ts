import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { MovieCardComponent } from './movie-card.component';

describe('MovieCardComponent', () => {
  let component: MovieCardComponent;
  let fixture: ComponentFixture<MovieCardComponent>;
  let routerSpy = { navigate: jasmine.createSpy('navigate') };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MovieCardComponent],
      providers: [{ provide: Router, useValue: routerSpy }],
    }).compileComponents();

    fixture = TestBed.createComponent(MovieCardComponent);
    component = fixture.componentInstance;
    component.movie = {
      id: 1,
      title: 'Test Movie',
      description: 'Test Description',
      imageUrl: 'test.jpg',
      genres: ['Action'],
      duration: '120',
      releaseDate: '2024-03-20',
    };
    fixture.detectChanges();
  });

  it('should navigate to movie details on click', () => {
    component.navigateToMovie();
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/film', 'Test Movie']);
  });
});
