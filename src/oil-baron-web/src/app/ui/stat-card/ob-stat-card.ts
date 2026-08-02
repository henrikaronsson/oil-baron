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
      class="border border-[var(--ob-border)] bg-[var(--ob-surface-document)] px-5 py-4 rounded-[var(--ob-radius-sm)] ob-raised-panel"
    >
      <div class="flex items-center gap-4">
        @if (icon) {
          <img [src]="icon" alt="" class="shrink-0" [class]="iconClass" aria-hidden="true" />
        }
        <div class="flex flex-col items-start pl-1">
          <p class="ob-label">{{ label }}</p>
          <p
            class="mt-1 font-mono text-xl font-semibold tabular-nums tracking-tight"
            [class]="TONE_CLASSES[tone]"
          >
            <ng-content />
          </p>
        </div>
      </div>
    </div>
  `,
})
export class ObStatCard {
  @Input({ required: true }) label!: string;
  @Input() tone: ObStatTone = 'default';
  @Input() icon = '';
  @Input() iconClass = 'h-12 w-12';

  protected readonly TONE_CLASSES = TONE_CLASSES;
}
