export interface OilFieldDto {
  x: number;
  y: number;
  owned: boolean;
  drilled: boolean;
  producing: boolean;
  purchasePrice: number;
  monthlyProduction: number;
  operatingCostPerMonth: number;
  estimatedReserves: number;
  remainingReserves: number | null;
}

export interface GameStateDto {
  id: string;
  companyName: string;
  seed: number;
  /** Turn index from game start (0 = first month). */
  month: number;
  /** In-world calendar year (epoch January 1972). */
  calendarYear: number;
  /** In-world calendar month 1–12. */
  calendarMonth: number;
  /** In-world calendar day (1 while turns are monthly). */
  calendarDay: number;
  cash: number;
  oilBarrels: number;
  oilPrice: number;
  gridSize: number;
  oilFields: OilFieldDto[];
}

export interface ErrorResponse {
  error: string;
}
