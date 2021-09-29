import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import {MoviesComponent} from './Movies/movies.component'
import {GenreComponent} from './Genre/genre.component';


const routes: Routes = [
{path:'Movies',component:MoviesComponent},
{path:'Genres',component:GenreComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
