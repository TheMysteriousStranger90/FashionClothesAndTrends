import { ClothingItem } from "./clothing-item";
import { User } from "./user";

export interface FavoriteItemDto {
  userId: string;
  user: User;
  clothingItemDtoId: string;
  clothingItemDto: ClothingItem;
}
