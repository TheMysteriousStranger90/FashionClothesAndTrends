export interface CreateClothingItem {
  name: string;
  description: string;
  price: number;
  gender: string;
  size: string;
  category: string;
  isInStock: boolean;
  pictureUrl?: string | null;
  brand: string;
}
