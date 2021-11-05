import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MovieService } from 'src/app/core/services/movie.service';
import { Cast } from 'src/app/shared/models/Cast';
import { Genre } from 'src/app/shared/models/genre';
import { Movie } from 'src/app/shared/models/movie';
import { Trailer } from 'src/app/shared/models/Trailer';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})
export class MovieDetailsComponent implements OnInit {

  movies!: Movie;
  id: number = 0;
  Title: string = '';
  // Tagline: string = '';
  // RunTime: number = 0;
  // ReleaseDate: number = 0;
  // Rating: number = 0;
  // Genres!: Genre;
  // Trailers!: Trailer;
  // Casts !: Cast;
  // Overview: string = '';
  // Revenue: number = 0;
  // Budget: number = 0;

  constructor(private activeRoute: ActivatedRoute, private movieService: MovieService) { }

  ngOnInit(): void {
    // ActivatedRoute => that will give us all the information of the current route/url
    // get the id from the URL 1, 2
    // then call our api, getMovieDetails method
    this.activeRoute.paramMap.subscribe
      (
        p => {
          this.id = Number(p.get('id'));
          console.log('movieId= ' + this.id);
          // call the api
          this.movieService.getMovieDetails(this.id)
            .subscribe(
              m => {
                this.movies = m;
                console.log(this.movies);
              }
            );
        }
      )
    console.log('inside movie details component');
  }

}