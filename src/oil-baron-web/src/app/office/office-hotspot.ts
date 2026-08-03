export type OfficeHotspotId =
  | 'map'
  | 'charts'
  | 'phone'
  | 'files'
  | 'papers'
  | 'computer';

export interface OfficeHotspotDetail {
  id: OfficeHotspotId;
  label: string;
  blurb: string;
  futureAction: string;
}

export const OFFICE_HOTSPOT_DETAILS: Record<OfficeHotspotId, OfficeHotspotDetail> = {
  map: {
    id: 'map',
    label: 'Texas lease map',
    blurb:
      'A printed outline of Texas on the blotter — lease blocks and pipeline corridors marked in grease pencil under the desk lamp.',
    futureAction: 'Open exploration and oil-field management.',
  },
  charts: {
    id: 'charts',
    label: 'Production charts',
    blurb:
      'A bar sheet of monthly barrels and cash burn, clipped from last quarter’s board pack. Red and olive columns climb unevenly across the page.',
    futureAction: 'Review production and financial charts.',
  },
  phone: {
    id: 'phone',
    label: 'Rotary telephone',
    blurb:
      'A grey desk phone with a coiled cord and a full rotary dial. The cradle still clicks when the handset settles.',
    futureAction: 'Place calls for deals, contacts, and negotiations.',
  },
  files: {
    id: 'files',
    label: 'Filing cabinet',
    blurb:
      'A metal cabinet of manila folders: leases, survey notes, and vendor invoices. The bottom drawer sticks when humidity rises.',
    futureAction: 'Browse paper records, leases, and contracts.',
  },
  papers: {
    id: 'papers',
    label: 'Newspaper & reports',
    blurb:
      'The morning oil pages and a stack of carbon-copy operating reports. Coffee rings mark the top sheet.',
    futureAction: 'Review monthly statements and industry news.',
  },
  computer: {
    id: 'computer',
    label: 'Desktop computer',
    blurb:
      'A beige all-in-one with a green CRT and a disk slot in the base. Market figures flicker whenever the timeshare line connects.',
    futureAction: 'Inspect market tape and operations data.',
  },
};

export interface OfficeHotspotProp {
  id: OfficeHotspotId;
  label: string;
  /** Public URL under /office. */
  imageSrc: string;
  /** Percentage of scene width/height for the prop button. */
  left: number;
  top: number;
  width: number;
  height: number;
}

/** Desk props — art from `public/office`. */
export const OFFICE_HOTSPOT_PROPS: OfficeHotspotProp[] = [
  {
    id: 'map',
    label: 'Texas lease map',
    imageSrc: 'office/texas-outline.svg',
    left: 6,
    top: 48,
    width: 24,
    height: 28,
  },
  {
    id: 'charts',
    label: 'Production charts',
    imageSrc: 'office/bar-graph.svg',
    left: 34,
    top: 50,
    width: 14,
    height: 20,
  },
  {
    id: 'phone',
    label: 'Rotary telephone',
    imageSrc: 'office/rotary-phone.svg',
    left: 52,
    top: 48,
    width: 12,
    height: 16,
  },
  {
    id: 'papers',
    label: 'Newspaper & reports',
    imageSrc: 'office/newspaper.svg',
    left: 46,
    top: 66,
    width: 14,
    height: 18,
  },
  {
    id: 'computer',
    label: 'Desktop computer',
    imageSrc: 'office/computer.svg',
    left: 66,
    top: 44,
    width: 18,
    height: 28,
  },
  {
    id: 'files',
    label: 'Filing cabinet',
    imageSrc: 'office/filing-cabinet.svg',
    left: 78,
    top: 66,
    width: 16,
    height: 28,
  },
];
