import { Guid } from "guid-typescript";

export interface Wishlist {
  id: Guid;
  name: string;
  wishListItems: WishlistItem[];
}

export interface WishlistItem {
  id: Guid;
  clothingItemName: string;
  clothingItemId: string;
  pictureUrl: string;
}
