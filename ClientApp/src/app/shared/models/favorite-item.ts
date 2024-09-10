import { Guid } from "guid-typescript";
import { ClothingItem } from "./clothing-item";
import { User } from "./user";

export interface FavoriteItem {
  userId: string;
  user: User;
  clothingItemId: Guid;
  clothingItem: ClothingItem;
}
