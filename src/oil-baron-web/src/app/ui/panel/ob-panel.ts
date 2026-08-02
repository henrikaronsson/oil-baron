import { Component, Input } from '@angular/core';

export type ObPanelVariant = 'corporate' | 'document' | 'terminal' | 'control';

const VARIANT_CLASSES: Record<ObPanelVariant, string> = {
  corporate:
    'border border-[var(--ob-border)] bg-[var(--ob-surface-paper)] ob-raised-panel',
  document:
    'border border-[var(--ob-border-strong)] bg-[var(--ob-surface-document)] ob-document-shadow ob-paper-grain',
  terminal:
    'border border-[var(--ob-border-strong)] bg-[var(--ob-surface-terminal)] text-[var(--ob-text-terminal)] ob-terminal-scan',
  control:
    'border border-[var(--ob-border)] bg-[var(--ob-surface-control)] ob-inset-panel',
};

@Component({
  selector: 'ob-panel',
  standalone: true,
  host: {
    class: 'block',
  },
  template: `
    <section [attr.aria-labelledby]="title ? headingId : null" [class]="panelClasses">
      @if (eyebrow || title) {
        <header class="border-b px-5 py-3" [class]="headerClasses">
          @if (eyebrow) {
            <p class="ob-label" [class]="eyebrowToneClass">{{ eyebrow }}</p>
          }
          @if (title) {
            <h2 [id]="headingId" class="mt-1 font-display text-xl tracking-[0.06em]" [class]="titleToneClass">
              {{ title }}
            </h2>
          }
        </header>
      }
      <div class="px-5 py-4" [class]="bodyClass">
        <ng-content />
      </div>
    </section>
  `,
})
export class ObPanel {
  private static nextId = 0;

  @Input() variant: ObPanelVariant = 'corporate';
  @Input() eyebrow = '';
  @Input() title = '';
  @Input() bodyClass = '';

  readonly headingId = `ob-panel-heading-${ObPanel.nextId++}`;

  get panelClasses(): string {
    return [VARIANT_CLASSES[this.variant], 'rounded-[var(--ob-radius-md)]'].join(' ');
  }

  get headerClasses(): string {
    if (this.variant === 'terminal') {
      return 'border-[var(--ob-border-strong)] bg-black/25';
    }
    if (this.variant === 'document') {
      return 'border-[var(--ob-divider-strong)] bg-[var(--ob-surface-document)]';
    }
    return 'border-[var(--ob-divider-strong)] bg-[var(--ob-surface-header)]';
  }

  get eyebrowToneClass(): string {
    return this.variant === 'terminal' ? 'text-[var(--ob-text-terminal)]' : '';
  }

  get titleToneClass(): string {
    return this.variant === 'terminal'
      ? 'text-[var(--ob-text-terminal)]'
      : 'text-[var(--ob-text-primary)]';
  }
}
