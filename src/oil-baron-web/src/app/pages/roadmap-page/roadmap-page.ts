import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ObBadge, ObBadgeVariant, ObPanel } from '../../ui';

export type RoadmapStratumStatus = 'struck' | 'drilling' | 'prospect';

export interface RoadmapItem {
  title: string;
  blurb: string;
}

export interface RoadmapStratum {
  depth: string;
  codename: string;
  status: RoadmapStratumStatus;
  statusLabel: string;
  badge: ObBadgeVariant;
  lead: string;
  icon: string;
  items: RoadmapItem[];
}

@Component({
  selector: 'app-roadmap-page',
  standalone: true,
  imports: [RouterLink, ObBadge, ObPanel],
  templateUrl: './roadmap-page.html',
  styleUrl: './roadmap-page.scss',
})
export class RoadmapPage {
  readonly strata: RoadmapStratum[] = [
    {
      depth: '01',
      codename: 'Pay dirt',
      status: 'struck',
      statusLabel: 'Struck',
      badge: 'success',
      lead: 'The first barrel is already on the books. The loop works: lease, drill, produce, sell.',
      icon: 'icons/petrol-industry/svg/028-oil.svg',
      items: [
        {
          title: 'Deterministic engine',
          blurb: 'Same seed, same commands, same outcome — rules live in OilBaron.Game.',
        },
        {
          title: 'In-memory API',
          blurb: 'Thin HTTP over the simulation; sessions evaporate when the process does.',
        },
        {
          title: 'Boardroom HUD',
          blurb: 'Angular client: cash, barrels, price, and a 3×3 lease board.',
        },
        {
          title: 'Economy tests',
          blurb: 'Unit coverage for legal moves, illegal moves, and determinism.',
        },
      ],
    },
    {
      depth: '02',
      codename: 'Wildcat stretch',
      status: 'drilling',
      statusLabel: 'Drilling',
      badge: 'warning',
      lead: 'Next hole down: make the company feel lived-in — memory, mystery, and sharper feedback.',
      icon: 'icons/petrol-industry/svg/029-drill.svg',
      items: [
        {
          title: 'Persist the books',
          blurb: 'SQLite (or similar) so a venture survives a browser refresh.',
        },
        {
          title: 'Survey & fog',
          blurb: 'Estimates lie a little. Remaining reserves stay secret until steel hits pay.',
        },
        {
          title: 'Dry-hole telegraph',
          blurb: 'Clearer misses, toasts, and the sting of a barren lease.',
        },
        {
          title: 'Wider acreage',
          blurb: 'Larger maps and varied terrain beyond the starter patch.',
        },
        {
          title: 'A rival on the wire',
          blurb: 'One AI company — same server rules, different appetite for risk.',
        },
      ],
    },
    {
      depth: '03',
      codename: 'Deep horizon',
      status: 'prospect',
      statusLabel: 'Prospect',
      badge: 'info',
      lead: 'Farther down the casing: empire toys. Only after the core loop sings.',
      icon: 'icons/petrol-industry/svg/003-pipe.svg',
      items: [
        {
          title: 'Shared leases',
          blurb: 'Multiplayer sessions without turning the rules into a committee.',
        },
        {
          title: 'Steel & tanks',
          blurb: 'Pipelines, storage caps, and a sniff of refining.',
        },
        {
          title: 'Desk as HQ',
          blurb: 'Wire the office hotspots — maps, files, phone — to real game flows.',
        },
        {
          title: 'Original oil paint',
          blurb: 'Polish, period art, and a hosted demo the public can strike.',
        },
      ],
    },
  ];
}
