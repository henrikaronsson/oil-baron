import { Routes } from '@angular/router';
import { GamePage } from './pages/game-page/game-page';
import { OfficePage } from './pages/office-page/office-page';
import { RoadmapPage } from './pages/roadmap-page/roadmap-page';
import { ShowcasePage } from './pages/showcase-page/showcase-page';

export const routes: Routes = [
  { path: '', component: GamePage },
  { path: 'office', component: OfficePage },
  { path: 'roadmap', component: RoadmapPage },
  { path: 'showcase', component: ShowcasePage },
  { path: '**', redirectTo: '' },
];
