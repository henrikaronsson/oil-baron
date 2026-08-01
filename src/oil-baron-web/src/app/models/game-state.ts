export interface PlotDto {
  x: number;
  y: number;
  owned: boolean;
  drilled: boolean;
  producing: boolean;
  remainingReserve: number | null;
}

export interface GameStateDto {
  id: string;
  companyName: string;
  seed: number;
  day: number;
  cash: number;
  oilBarrels: number;
  oilPrice: number;
  gridSize: number;
  plots: PlotDto[];
}

export interface ErrorResponse {
  error: string;
}
