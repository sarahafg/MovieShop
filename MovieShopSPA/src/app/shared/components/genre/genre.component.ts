import { Component, Input, OnInit } from '@angular/core';
import { Genre } from '../../models/genre';

@Component({
  selector: 'app-genre',
  templateUrl: './genre.component.html',
  styleUrls: ['./genre.component.css']
})
export class GenreComponent implements OnInit {

  // will receive the data from parent component through @Input

  @Input() genre!: Genre
  constructor() { }

  ngOnInit(): void {
  }

}
