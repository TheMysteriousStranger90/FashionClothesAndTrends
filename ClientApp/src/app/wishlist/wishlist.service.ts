import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {environment} from 'src/environments/environment';
import {Wishlist, WishlistItem} from '../shared/models/wishlist';

@Injectable({
  providedIn: 'root'
})
export class WishlistService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  createWishlist(userId: string, wishlistName: string): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/wishlist`, {userId, wishlistName});
  }

  getWishlistsByUserId(userId: string): Observable<Wishlist[]> {
    return this.http.get<Wishlist[]>(`${this.baseUrl}/wishlist/user/${userId}`);
  }

  getWishlistByName(userId: string, wishlistName: string): Observable<Wishlist> {
    return this.http.get<Wishlist>(`${this.baseUrl}/wishlist/user/${userId}/name/${wishlistName}`);
  }

  deleteWishlist(wishlistId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/wishlist/${wishlistId}`);
  }

  addItemToWishlist(clothingItemId: string, wishlistName?: string): Observable<WishlistItem> {
    return this.http.post<WishlistItem>(`${this.baseUrl}/wishlist/items`, {clothingItemId, wishlistName});
  }

  removeItemFromWishlist(wishlistId: string, itemId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/wishlist/${wishlistId}/items/${itemId}`);
  }
}
