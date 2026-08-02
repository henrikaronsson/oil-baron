import { DecimalPipe } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, computed, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Observable } from 'rxjs';
import { GameStateDto, PlotDto } from '../../models/game-state';
import { GameApiService } from '../../services/game-api.service';
import {
  ObAlert,
  ObBadge,
  ObBadgeVariant,
  ObButton,
  ObFormField,
  ObMoneyValue,
  ObPanel,
  ObStatCard,
  ObToolbar,
  OB_INPUT_CLASSES,
} from '../../ui';

@Component({
  selector: 'app-game-page',
  imports: [
    FormsModule,
    DecimalPipe,
    ObAlert,
    ObBadge,
    ObButton,
    ObFormField,
    ObMoneyValue,
    ObPanel,
    ObStatCard,
    ObToolbar,
  ],
  templateUrl: './game-page.html',
  styleUrl: './game-page.scss',
})
export class GamePage {
  private readonly api = inject(GameApiService);

  readonly game = signal<GameStateDto | null>(null);
  readonly error = signal<string | null>(null);
  readonly busy = signal(false);
  readonly inputClasses = OB_INPUT_CLASSES;

  companyName = 'Acme Oil';
  seedInput = '';

  readonly rows = computed(() => {
    const state = this.game();
    if (!state) {
      return [] as PlotDto[][];
    }

    const size = state.gridSize;
    const grid: PlotDto[][] = [];
    for (let y = 0; y < size; y++) {
      const row: PlotDto[] = [];
      for (let x = 0; x < size; x++) {
        const plot = state.plots.find((p) => p.x === x && p.y === y);
        if (plot) {
          row.push(plot);
        }
      }
      grid.push(row);
    }
    return grid;
  });

  readonly producingCount = computed(
    () => this.game()?.plots.filter((p) => p.producing).length ?? 0,
  );

  startGame(): void {
    const name = this.companyName.trim();
    if (!name) {
      this.error.set('Company name is required.');
      return;
    }

    const seed =
      this.seedInput.trim() === '' ? null : Number.parseInt(this.seedInput.trim(), 10);
    if (seed != null && Number.isNaN(seed)) {
      this.error.set('Seed must be a whole number.');
      return;
    }

    this.run(() => this.api.createGame(name, seed));
  }

  buy(plot: PlotDto): void {
    const id = this.game()?.id;
    if (!id) {
      return;
    }
    this.run(() => this.api.buyPlot(id, plot.x, plot.y));
  }

  drill(plot: PlotDto): void {
    const id = this.game()?.id;
    if (!id) {
      return;
    }
    this.run(() => this.api.drillPlot(id, plot.x, plot.y));
  }

  advanceDay(): void {
    const id = this.game()?.id;
    if (!id) {
      return;
    }
    this.run(() => this.api.advanceDay(id));
  }

  sellOil(): void {
    const id = this.game()?.id;
    if (!id) {
      return;
    }
    this.run(() => this.api.sellOil(id));
  }

  newGame(): void {
    this.game.set(null);
    this.error.set(null);
  }

  plotStatus(plot: PlotDto): string {
    if (!plot.owned) {
      return 'Available';
    }
    if (!plot.drilled) {
      return 'Owned';
    }
    if (plot.producing) {
      return `Producing (${plot.remainingReserve} left)`;
    }
    return 'Dry / depleted';
  }

  plotBadgeVariant(plot: PlotDto): ObBadgeVariant {
    if (!plot.owned) {
      return 'neutral';
    }
    if (plot.producing) {
      return 'success';
    }
    if (plot.drilled) {
      return 'warning';
    }
    return 'info';
  }

  plotBadgeLabel(plot: PlotDto): string {
    if (!plot.owned) {
      return 'Open';
    }
    if (plot.producing) {
      return 'Flowing';
    }
    if (plot.drilled) {
      return 'Dry';
    }
    return 'Held';
  }

  private run(request: () => Observable<GameStateDto>): void {
    this.busy.set(true);
    this.error.set(null);
    request().subscribe({
      next: (state) => {
        this.game.set(state);
        this.busy.set(false);
      },
      error: (err: unknown) => {
        this.busy.set(false);
        this.error.set(this.readError(err));
      },
    });
  }

  private readError(err: unknown): string {
    if (err instanceof HttpErrorResponse) {
      const body = err.error as { error?: string } | string | null;
      if (typeof body === 'string' && body.trim()) {
        return body;
      }
      if (body && typeof body === 'object' && body.error) {
        return body.error;
      }
      if (err.status === 0) {
        return 'Cannot reach API. Is OilBaron.Api running on http://localhost:5080?';
      }
      return err.message;
    }
    return 'Something went wrong.';
  }
}
