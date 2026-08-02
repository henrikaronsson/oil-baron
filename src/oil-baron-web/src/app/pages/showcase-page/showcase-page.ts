import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import {
  ObAlert,
  ObBadge,
  ObButton,
  ObFormField,
  ObMoneyValue,
  ObPanel,
  ObStatCard,
  ObStatusLamp,
  ObToolbar,
  OB_INPUT_CLASSES,
} from '../../ui';

@Component({
  selector: 'app-showcase-page',
  standalone: true,
  imports: [
    RouterLink,
    ObAlert,
    ObBadge,
    ObButton,
    ObFormField,
    ObMoneyValue,
    ObPanel,
    ObStatCard,
    ObStatusLamp,
    ObToolbar,
  ],
  templateUrl: './showcase-page.html',
})
export class ShowcasePage {
  protected readonly inputClasses = OB_INPUT_CLASSES;
}