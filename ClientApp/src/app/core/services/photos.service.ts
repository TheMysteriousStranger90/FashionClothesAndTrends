import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {ClothingPhoto} from 'src/app/shared/models/clothing-photo';
import {UserPhoto} from 'src/app/shared/models/user-photo';
import {environment} from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PhotosService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  // User Photos
  getUserPhotoById(photoId: string): Observable<UserPhoto> {
    return this.http.get<UserPhoto>(`${this.baseUrl}photos/user/${photoId}`);
  }

  addUserPhoto(file: File): Observable<UserPhoto> {
    const formData = new FormData();
    formData.append('file', file);
    return this.http.post<UserPhoto>(`${this.baseUrl}photos/user`, formData);
  }

  setMainUserPhoto(photoId: string): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}photos/user/${photoId}/set-main`, {});
  }

  deleteUserPhoto(photoId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}photos/user/${photoId}`);
  }

  // Clothing Item Photos
  getClothingItemPhotoById(photoId: string): Observable<ClothingPhoto> {
    return this.http.get<ClothingPhoto>(`${this.baseUrl}photos/clothing-item/${photoId}`);
  }

  addClothingItemPhoto(clothingItemId: string, file: File): Observable<ClothingPhoto> {
    const formData = new FormData();
    formData.append('file', file);
    return this.http.post<ClothingPhoto>(`${this.baseUrl}photos/clothing-item/${clothingItemId}`, formData);
  }

  setMainClothingItemPhoto(clothingItemId: string, photoId: string): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}photos/clothing-item/${clothingItemId}/photo/${photoId}/set-main`, {});
  }

  deleteClothingItemPhoto(clothingItemId: string, photoId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}photos/clothing-item/${clothingItemId}/photo/${photoId}`);
  }
}
