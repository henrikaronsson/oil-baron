import { Component, computed, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { OfficeDeskScene } from '../../office/office-desk-scene';
import {
  OFFICE_HOTSPOT_DETAILS,
  OfficeHotspotDetail,
  OfficeHotspotId,
} from '../../office/office-hotspot';
import { ObPanel } from '../../ui';

@Component({
  selector: 'app-office-page',
  standalone: true,
  imports: [RouterLink, OfficeDeskScene, ObPanel],
  templateUrl: './office-page.html',
})
export class OfficePage {
  protected readonly selectedId = signal<OfficeHotspotId | null>(null);

  protected readonly selectedDetail = computed<OfficeHotspotDetail | null>(() => {
    const id = this.selectedId();
    return id ? OFFICE_HOTSPOT_DETAILS[id] : null;
  });

  protected onHotspotSelect(id: OfficeHotspotId): void {
    this.selectedId.set(id);
  }
}
