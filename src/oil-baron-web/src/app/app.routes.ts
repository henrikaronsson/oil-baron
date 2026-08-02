import { Routes } from '@angular/router';
import { GamePage } from './pages/game-page/game-page';
import { ShowcasePage } from './pages/showcase-page/showcase-page';

export const routes: Routes = [
  { path: '', component: GamePage },
  { path: 'showcase', component: ShowcasePage },
  { path: '**', redirectTo: '' },
];
