import { Component, Input } from '@angular/core';

export type ObStatusLampTone = 'idle' | 'ok' | 'warn' | 'alarm' | 'oil';

const TONE_CLASSES: Record<ObStatusLampTone, string> = {
  idle: 'bg-[var(--ob-surface-control)] border-[var(--ob-border-strong)]',
  ok: 'bg-[var(--ob-state-success)] border-[var(--ob-state-success-border)]',
  warn: 'bg-[var(--ob-action-warning)] border-[var(--ob-action-warning-border)]',
  alarm: 'bg-[var(--ob-action-danger)] border-[var(--ob-action-danger-border)]',
  oil: 'bg-[var(--ob-indicator-oil)] border-[var(--ob-border-strong)]',
};

@Component({
  selector: 'ob-status-lamp',
  standalone: true,
  template: `
    <span class="inline-flex items-center gap-2">
      <span
        class="inline-block h-2.5 w-2.5 rounded-full border"
        [class]="TONE_CLASSES[tone]"
        aria-hidden="true"
      ></span>
      @if (label) {
        <span class="text-sm text-[var(--ob-text-secondary)]">{{ label }}</span>
      }
    </span>
  `,
})
export class ObStatusLamp {
  @Input() tone: ObStatusLampTone = 'idle';
  @Input() label = '';

  protected readonly TONE_CLASSES = TONE_CLASSES;
}
