import { Component, Input, OnInit } from '@angular/core';
import { Movie } from 'src/app/shared/models/movie';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.css']
})
export class MovieComponent implements OnInit {

  // will receive the data from parent component through @Input

  @Input() movie!: Movie
  constructor() { }

  ngOnInit(): void {
  }

}
