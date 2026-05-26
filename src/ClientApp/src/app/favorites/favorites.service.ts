import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Guid } from 'guid-typescript';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { FavoriteItemDto } from '../shared/models/favorite-item';

@Injectable({
  providedIn: 'root'
})
export class FavoritesService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  addFavorite(clothingItemId: string): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}favorites/${clothingItemId}`, {});
  }

  removeFavorite(clothingItemId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}favorites/${clothingItemId}`);
  }

  getFavoritesByUserId(): Observable<FavoriteItemDto[]> {
    return this.http.get<FavoriteItemDto[]>(`${this.baseUrl}favorites/user`);
  }

  isFavorite(clothingItemId: string): Observable<boolean> {
    return this.http.get<boolean>(`${this.baseUrl}favorites/${clothingItemId}/isFavorite`);
  }
}
