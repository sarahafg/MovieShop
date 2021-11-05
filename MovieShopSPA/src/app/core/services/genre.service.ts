import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Genre } from 'src/app/shared/models/genre';

@Injectable({
  providedIn: 'root'
})
export class GenreService {

  constructor(private http: HttpClient) { }

  //https://localhost:44330/api/Movies/toprevenue
  // many methods that will be used by components
  // HomeComponent will call this function
  getAllGenres() : Observable<Genre[]> {
    // call our API using HttpClient (XMLHttpRequest) to make GET request
    // HttpClient class comes from HttpClientModule (Angular Team created for us to use)
    // import HttpClientModule inside AppModule
  
    return this.http.get<Genre[]>('https://localhost:44330/api/Genres');
  }
}
