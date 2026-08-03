import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { GameStateDto } from '../models/game-state';

const API_BASE = 'http://localhost:5080/api';

@Injectable({ providedIn: 'root' })
export class GameApiService {
  private readonly http = inject(HttpClient);

  createGame(companyName: string, seed?: number | null): Observable<GameStateDto> {
    const body: { companyName: string; seed?: number } = { companyName };
    if (seed != null && !Number.isNaN(seed)) {
      body.seed = seed;
    }
    return this.http.post<GameStateDto>(`${API_BASE}/games`, body);
  }

  getGame(id: string): Observable<GameStateDto> {
    return this.http.get<GameStateDto>(`${API_BASE}/games/${id}`);
  }

  buyField(id: string, x: number, y: number): Observable<GameStateDto> {
    return this.http.post<GameStateDto>(`${API_BASE}/games/${id}/fields/${x}/${y}/buy`, {});
  }

  drillField(id: string, x: number, y: number): Observable<GameStateDto> {
    return this.http.post<GameStateDto>(`${API_BASE}/games/${id}/fields/${x}/${y}/drill`, {});
  }

  advanceMonth(id: string): Observable<GameStateDto> {
    return this.http.post<GameStateDto>(`${API_BASE}/games/${id}/advance-month`, {});
  }

  sellOil(id: string): Observable<GameStateDto> {
    return this.http.post<GameStateDto>(`${API_BASE}/games/${id}/sell-oil`, {});
  }
}
