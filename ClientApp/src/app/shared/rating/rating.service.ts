import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Guid } from 'guid-typescript';
import { RatingDto } from '../models/rating';

@Injectable({
  providedIn: 'root'
})
export class RatingService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  addRating(rating: RatingDto): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}rating/${rating}`, {});
  }

  updateRating(rating: RatingDto): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}rating`, rating);
  }

  getAverageRating(clothingItemId: string): Observable<number> {
    return this.http.get<number>(`${this.baseUrl}rating/clothing/${clothingItemId}/average`);
  }

  getUserRating(userId: string, clothingItemId: string): Observable<RatingDto | null> {
    return this.http.get<RatingDto | null>(`${this.baseUrl}rating/user-rating/${clothingItemId}`);
  }
}
