import { ClothingPhoto } from "./clothing-photo";

export interface ClothingItem {
  id: string;
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
  clothingItemPhotos: ClothingPhoto[];
}

export class ClothingItem implements ClothingItem {}
