import { Component, Input } from '@angular/core';

export type ObBadgeVariant = 'neutral' | 'success' | 'warning' | 'danger' | 'info' | 'oil';

const VARIANT_CLASSES: Record<ObBadgeVariant, string> = {
  neutral:
    'border-[var(--ob-border)] bg-[var(--ob-surface-raised)] text-[var(--ob-text-secondary)]',
  success:
    'border-[var(--ob-state-success-border)] bg-[var(--ob-state-success-bg)] text-[var(--ob-state-success)]',
  warning:
    'border-[var(--ob-state-warning-border)] bg-[var(--ob-state-warning-bg)] text-[var(--ob-state-warning)]',
  danger:
    'border-[var(--ob-state-danger-border)] bg-[var(--ob-state-danger-bg)] text-[var(--ob-state-danger)]',
  info:
    'border-[var(--ob-state-info-border)] bg-[var(--ob-state-info-bg)] text-[var(--ob-state-info)]',
  oil:
    'border-[var(--ob-border-strong)] bg-[var(--ob-indicator-oil)] text-[var(--ob-text-inverse)]',
};

@Component({
  selector: 'ob-badge',
  standalone: true,
  template: `
    <span
      class="inline-flex items-center border px-2 py-0.5 text-xs font-semibold uppercase tracking-[0.1em] rounded-[var(--ob-radius-sm)]"
      [class]="VARIANT_CLASSES[variant]"
    >
      <ng-content />
    </span>
  `,
})
export class ObBadge {
  @Input() variant: ObBadgeVariant = 'neutral';

  protected readonly VARIANT_CLASSES = VARIANT_CLASSES;
}
