import { Component, input, output } from '@angular/core';
import { OFFICE_HOTSPOT_PROPS, OfficeHotspotId } from './office-hotspot';

@Component({
  selector: 'app-office-desk-scene',
  standalone: true,
  templateUrl: './office-desk-scene.html',
  styleUrl: './office-desk-scene.scss',
})
export class OfficeDeskScene {
  readonly selectedId = input<OfficeHotspotId | null>(null);
  readonly hotspotSelect = output<OfficeHotspotId>();

  protected readonly props = OFFICE_HOTSPOT_PROPS;

  protected select(id: OfficeHotspotId): void {
    this.hotspotSelect.emit(id);
  }

  protected isSelected(id: OfficeHotspotId): boolean {
    return this.selectedId() === id;
  }
}
