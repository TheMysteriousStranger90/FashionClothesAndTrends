import { Guid } from "guid-typescript";

export interface ClothingItem {
  id: Guid;
  name: string;
  description: string;
  price: number;
  gender: string;
  size: string;
  category: string;
  discount: number | null;
  isInStock: boolean;
  pictureUrl: string;
  brand: string;
}

export class ClothingItem implements ClothingItem {}
