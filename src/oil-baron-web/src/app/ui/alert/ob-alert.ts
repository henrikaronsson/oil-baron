import { Component, Input } from '@angular/core';

export type ObAlertVariant = 'danger' | 'warning' | 'success' | 'info';

const VARIANT_CLASSES: Record<ObAlertVariant, string> = {
  danger:
    'border-[var(--ob-state-danger-border)] bg-[var(--ob-state-danger-bg)] text-[var(--ob-state-danger)]',
  warning:
    'border-[var(--ob-state-warning-border)] bg-[var(--ob-state-warning-bg)] text-[var(--ob-state-warning)]',
  success:
    'border-[var(--ob-state-success-border)] bg-[var(--ob-state-success-bg)] text-[var(--ob-state-success)]',
  info:
    'border-[var(--ob-state-info-border)] bg-[var(--ob-state-info-bg)] text-[var(--ob-state-info)]',
};

const VARIANT_LABELS: Record<ObAlertVariant, string> = {
  danger: 'Alert',
  warning: 'Notice',
  success: 'Confirmed',
  info: 'Information',
};

@Component({
  selector: 'ob-alert',
  standalone: true,
  template: `
    <div
      role="alert"
      class="border px-4 py-3 rounded-[var(--ob-radius-sm)]"
      [class]="VARIANT_CLASSES[variant]"
    >
      <p class="ob-label mb-1">{{ VARIANT_LABELS[variant] }}</p>
      <div class="font-medium text-[var(--ob-text-primary)]">
        <ng-content />
      </div>
    </div>
  `,
})
export class ObAlert {
  @Input() variant: ObAlertVariant = 'info';

  protected readonly VARIANT_CLASSES = VARIANT_CLASSES;
  protected readonly VARIANT_LABELS = VARIANT_LABELS;
}
