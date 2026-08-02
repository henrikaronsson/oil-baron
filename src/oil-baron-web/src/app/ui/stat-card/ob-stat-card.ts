import { Component, Input } from '@angular/core';

export type ObStatTone = 'default' | 'cash' | 'oil' | 'production' | 'market';

const TONE_CLASSES: Record<ObStatTone, string> = {
  default: 'text-[var(--ob-text-primary)]',
  cash: 'text-[var(--ob-indicator-cash)]',
  oil: 'text-[var(--ob-indicator-oil)]',
  production: 'text-[var(--ob-indicator-production)]',
  market: 'text-[var(--ob-indicator-market)]',
};

@Component({
  selector: 'ob-stat-card',
  standalone: true,
  template: `
    <div
      class="border border-[var(--ob-border)] bg-[var(--ob-surface-raised)] px-4 py-3 rounded-[var(--ob-radius-sm)] ob-raised-panel"
    >
      <div class="flex items-center gap-2">
        @if (showLamp) {
          <span
            class="inline-block h-2.5 w-2.5 rounded-full border border-[var(--ob-border-strong)]"
            [class]="lampClass"
            aria-hidden="true"
          ></span>
        }
        <p class="ob-label">{{ label }}</p>
      </div>
      <p
        class="mt-1 font-mono text-lg font-semibold tabular-nums tracking-tight"
        [class]="TONE_CLASSES[tone]"
      >
        <ng-content />
      </p>
    </div>
  `,
})
export class ObStatCard {
  @Input({ required: true }) label!: string;
  @Input() tone: ObStatTone = 'default';
  @Input() showLamp = false;

  protected readonly TONE_CLASSES = TONE_CLASSES;

  get lampClass(): string {
    switch (this.tone) {
      case 'cash':
        return 'bg-[var(--ob-indicator-cash)]';
      case 'oil':
        return 'bg-[var(--ob-indicator-oil)]';
      case 'production':
        return 'bg-[var(--ob-indicator-production)]';
      case 'market':
        return 'bg-[var(--ob-indicator-market)]';
      default:
        return 'bg-[var(--ob-surface-control)]';
    }
  }
}
