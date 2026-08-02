import { Component, Input } from '@angular/core';

@Component({
  selector: 'ob-toolbar',
  standalone: true,
  host: {
    class: 'block',
  },
  template: `
    <div
      class="flex flex-wrap items-center gap-2 border border-[var(--ob-border)] bg-[var(--ob-surface-control)] px-3 py-2 rounded-[var(--ob-radius-sm)] ob-inset-panel"
      [attr.aria-label]="ariaLabel"
    >
      @if (label) {
        <span class="ob-label mr-2">{{ label }}</span>
      }
      <ng-content />
    </div>
  `,
})
export class ObToolbar {
  @Input() label = '';
  @Input() ariaLabel = 'Toolbar';
}
