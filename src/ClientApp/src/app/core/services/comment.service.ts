import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Comment } from '../../shared/models/comment';
@Injectable({
  providedIn: 'root'
})
export class CommentService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  addComment(comment: Comment): Observable<Comment> {
    return this.http.post<Comment>(`${this.baseUrl}comments`, comment);
  }

  removeComment(commentId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}comments/${commentId}`);
  }

  getCommentsForClothingItem(clothingItemId: string): Observable<Comment[]> {
    return this.http.get<Comment[]>(`${this.baseUrl}comments/clothing/${clothingItemId}`);
  }

  getCommentsByUserId(userId: string): Observable<Comment[]> {
    return this.http.get<Comment[]>(`${this.baseUrl}comments/users/${userId}`);
  }
}
