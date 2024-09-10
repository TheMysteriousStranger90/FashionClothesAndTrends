import { Guid } from "guid-typescript";

export interface ClothingPhoto {
  id: Guid;
  url: string;
  isMain: boolean;
}
