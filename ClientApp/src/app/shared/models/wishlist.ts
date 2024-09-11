
export interface Wishlist {
  id: string;
  name: string;
  wishListItems: WishlistItem[];
}

export interface WishlistItem {
  id: string;
  clothingItemName: string;
  clothingItemId: string;
  pictureUrl: string;
}
