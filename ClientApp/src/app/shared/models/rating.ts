import { Guid } from "guid-typescript";

export interface Rating {
  userId: string;
  username: string;
  clothingItemId: Guid;
  score: number;
}
