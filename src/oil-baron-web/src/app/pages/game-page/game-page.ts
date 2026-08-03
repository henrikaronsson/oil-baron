import { DecimalPipe } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, computed, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { Observable } from 'rxjs';
import { GameStateDto, OilFieldDto } from '../../models/game-state';
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
    RouterLink,
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
      return [] as OilFieldDto[][];
    }

    const size = state.gridSize;
    const grid: OilFieldDto[][] = [];
    for (let y = 0; y < size; y++) {
      const row: OilFieldDto[] = [];
      for (let x = 0; x < size; x++) {
        const field = state.oilFields.find((f) => f.x === x && f.y === y);
        if (field) {
          row.push(field);
        }
      }
      grid.push(row);
    }
    return grid;
  });

  readonly producingCount = computed(
    () => this.game()?.oilFields.filter((f) => f.producing).length ?? 0,
  );

  private static readonly monthNames = [
    'Jan',
    'Feb',
    'Mar',
    'Apr',
    'May',
    'Jun',
    'Jul',
    'Aug',
    'Sep',
    'Oct',
    'Nov',
    'Dec',
  ] as const;

  /** Renders server calendar fields as "Jan 1". */
  formatCalendarDate(state: GameStateDto): string {
    const monthName = GamePage.monthNames[state.calendarMonth - 1];
    if (
      state.calendarYear == null ||
      state.calendarMonth == null ||
      state.calendarDay == null ||
      !monthName
    ) {
      return '—';
    }
    return `${monthName} ${state.calendarDay}`;
  }

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

  buy(field: OilFieldDto): void {
    const id = this.game()?.id;
    if (!id) {
      return;
    }
    this.run(() => this.api.buyField(id, field.x, field.y));
  }

  drill(field: OilFieldDto): void {
    const id = this.game()?.id;
    if (!id) {
      return;
    }
    this.run(() => this.api.drillField(id, field.x, field.y));
  }

  advanceMonth(): void {
    const id = this.game()?.id;
    if (!id) {
      return;
    }
    this.run(() => this.api.advanceMonth(id));
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

  fieldStatus(field: OilFieldDto): string {
    if (!field.owned) {
      return 'Available';
    }
    if (!field.drilled) {
      return 'Owned';
    }
    if (field.producing) {
      return `Producing (${field.remainingReserves} left)`;
    }
    return 'Dry / depleted';
  }

  fieldBadgeVariant(field: OilFieldDto): ObBadgeVariant {
    if (!field.owned) {
      return 'neutral';
    }
    if (field.producing) {
      return 'success';
    }
    if (field.drilled) {
      return 'warning';
    }
    return 'info';
  }

  fieldBadgeLabel(field: OilFieldDto): string {
    if (!field.owned) {
      return 'Open';
    }
    if (field.producing) {
      return 'Flowing';
    }
    if (field.drilled) {
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
