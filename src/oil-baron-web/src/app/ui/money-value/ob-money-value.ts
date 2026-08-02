import { CurrencyPipe } from '@angular/common';
import { Component, Input } from '@angular/core';

export type ObMoneyTone = 'default' | 'positive' | 'negative';

const TONE_CLASSES: Record<ObMoneyTone, string> = {
  default: 'text-[var(--ob-text-primary)]',
  positive: 'text-[var(--ob-money-positive)]',
  negative: 'text-[var(--ob-money-negative)]',
};

@Component({
  selector: 'ob-money-value',
  standalone: true,
  imports: [CurrencyPipe],
  template: `
    <span
      class="font-mono font-semibold tabular-nums tracking-tight"
      [class]="TONE_CLASSES[tone]"
      [attr.aria-label]="ariaLabel"
    >
      {{ amount | currency: currencyCode : 'symbol' : digitsInfo }}
    </span>
  `,
})
export class ObMoneyValue {
  @Input({ required: true }) amount!: number;
  @Input() currencyCode = 'USD';
  @Input() digitsInfo = '1.0-0';
  @Input() tone: ObMoneyTone = 'default';
  @Input() label = '';

  protected readonly TONE_CLASSES = TONE_CLASSES;

  get ariaLabel(): string | null {
    if (!this.label) {
      return null;
    }
    return `${this.label}: ${this.amount}`;
  }
}
