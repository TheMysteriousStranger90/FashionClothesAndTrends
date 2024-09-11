import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Rating } from '../shared/models/rating';
import { Observable } from 'rxjs';
import { Guid } from 'guid-typescript';

@Injectable({
  providedIn: 'root'
})
export class RatingService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  addRating(rating: Rating): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}rating`, rating);
  }

  updateRating(rating: Rating): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}rating`, rating);
  }

  getAverageRating(clothingItemId: string): Observable<number | null> {
    return this.http.get<number | null>(`${this.baseUrl}rating/clothing/${clothingItemId}/average`);
  }
}
