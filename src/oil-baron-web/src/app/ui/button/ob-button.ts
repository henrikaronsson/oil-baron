import { Component, Input } from '@angular/core';

export type ObButtonVariant =
  | 'primary'
  | 'secondary'
  | 'financial'
  | 'warning'
  | 'danger'
  | 'quiet'
  | 'terminal';

export type ObButtonSize = 'sm' | 'md';

const VARIANT_CLASSES: Record<ObButtonVariant, string> = {
  primary:
    'border-[var(--ob-action-primary-border)] bg-[var(--ob-action-primary)] text-[var(--ob-action-primary-text)] hover:bg-[var(--ob-action-primary-hover)]',
  secondary:
    'border-[var(--ob-action-secondary-border)] bg-[var(--ob-action-secondary)] text-[var(--ob-action-secondary-text)] hover:bg-[var(--ob-action-secondary-hover)]',
  financial:
    'border-[var(--ob-action-financial-border)] bg-[var(--ob-action-financial)] text-[var(--ob-action-financial-text)] hover:bg-[var(--ob-action-financial-hover)]',
  warning:
    'border-[var(--ob-action-warning-border)] bg-[var(--ob-action-warning)] text-[var(--ob-action-warning-text)] hover:bg-[var(--ob-action-warning-hover)]',
  danger:
    'border-[var(--ob-action-danger-border)] bg-[var(--ob-action-danger)] text-[var(--ob-action-danger-text)] hover:bg-[var(--ob-action-danger-hover)]',
  quiet:
    'border-transparent bg-transparent text-[var(--ob-action-quiet-text)] hover:bg-[var(--ob-action-quiet-hover)]',
  terminal:
    'border-[var(--ob-action-terminal-border)] bg-[var(--ob-action-terminal)] font-mono text-[var(--ob-action-terminal-text)] hover:bg-[var(--ob-action-terminal-hover)]',
};

const SIZE_CLASSES: Record<ObButtonSize, string> = {
  sm: 'px-2.5 py-1 text-sm',
  md: 'px-4 py-2 text-base',
};

@Component({
  selector: 'ob-button',
  standalone: true,
  host: {
    class: 'inline-flex',
  },
  template: `
    <button
      [attr.type]="type"
      [disabled]="disabled"
      [class]="classes"
    >
      <ng-content />
    </button>
  `,
})
export class ObButton {
  @Input() variant: ObButtonVariant = 'primary';
  @Input() size: ObButtonSize = 'md';
  @Input() type: 'button' | 'submit' | 'reset' = 'button';
  @Input() disabled = false;

  get classes(): string {
    const base =
      'inline-flex w-full items-center justify-center gap-2 border font-semibold tracking-wide transition ' +
      'focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 ' +
      'focus-visible:outline-[var(--ob-focus)] disabled:cursor-not-allowed disabled:opacity-50 ' +
      'rounded-[var(--ob-radius-sm)]';

    return [base, VARIANT_CLASSES[this.variant], SIZE_CLASSES[this.size]].join(' ');
  }
}
