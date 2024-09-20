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

  createWishlist(wishlistName: string): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}wishlist/${wishlistName}`, {});
  }

  getWishlistsByUserId(): Observable<Wishlist[]> {
    return this.http.get<Wishlist[]>(`${this.baseUrl}wishlist/user/`);
  }

  getWishlistByName(userId: string, wishlistName: string): Observable<Wishlist> {
    return this.http.get<Wishlist>(`${this.baseUrl}wishlist/user/${userId}/name/${wishlistName}`);
  }

  deleteWishlist(wishlistId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}wishlist/${wishlistId}`);
  }

  addItemToWishlist(clothingItemId: string, wishlistId: string): Observable<WishlistItem> {
    return this.http.post<WishlistItem>(`${this.baseUrl}wishlist/${wishlistId}/items/${clothingItemId}`, {});
  }

  removeItemFromWishlist(wishlistId: string, itemId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}wishlist/${wishlistId}/items/${itemId}`);
  }
}
