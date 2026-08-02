import { Component, Input } from '@angular/core';

@Component({
  selector: 'ob-form-field',
  standalone: true,
  template: `
    <div class="space-y-1.5">
      <label class="ob-label block" [attr.for]="controlId">{{ label }}</label>
      <ng-content />
      @if (hint) {
        <p class="text-sm text-[var(--ob-text-muted)]">{{ hint }}</p>
      }
    </div>
  `,
})
export class ObFormField {
  @Input({ required: true }) label!: string;
  @Input({ required: true }) controlId!: string;
  @Input() hint = '';
}
