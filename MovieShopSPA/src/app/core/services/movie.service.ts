import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { MovieCard } from 'src/app/shared/models/moviecard';
// import { Movie } from 'src/app/shared/models/movie';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  constructor(private http: HttpClient) { }

//https://localhost:44330/api/Movies/toprevenue
// many methods that will be used by components
// HomeComponent will call this function
getTopRevenueMovies(): Observable<MovieCard[]> {
  // call our API using HttpClient (XMLHttpRequest) to make GET request
  // HttpClient class comes from HttpClientModule (Angular Team created for us to use)
  // import HttpClientModule inside AppModule

  return this.http.get<MovieCard[]>('https://localhost:44330/api/Movies/toprevenue');
}
getTopRatedMovies(): Observable<MovieCard[]> {
  // call our API using HttpClient (XMLHttpRequest) to make GET request
  // HttpClient class comes from HttpClientModule (Angular Team created for us to use)
  // import HttpClientModule inside AppModule

  return this.http.get<MovieCard[]>('https://localhost:44330/api/Movies/toprated');
}
// getMovieDetails(id: number): Observable<Movie> {
//   // call our API using HttpClient (XMLHttpRequest) to make GET request
//   // HttpClient class comes from HttpClientModule (Angular Team created for us to use)
//   // import HttpClientModule inside AppModule

//   return this.http.get<Movie>('https://localhost:44330/api/Movies');
// }

}
