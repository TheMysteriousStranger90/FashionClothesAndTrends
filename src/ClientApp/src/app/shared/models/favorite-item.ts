import { ClothingItem } from "./clothing-item";
import { User } from "./user";

export interface FavoriteItemDto {
  userDtoId: string;
  userDto: User;
  clothingItemDtoId: string;
  clothingItemDto: ClothingItem;
}
