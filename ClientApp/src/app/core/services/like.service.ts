import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LikeDislike } from 'src/app/shared/models/like-dislike';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LikeService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  addLikeDislike(likeDislike: LikeDislike): Observable<LikeDislike> {
    return this.http.post<LikeDislike>(`${this.baseUrl}likes`, likeDislike);
  }

  removeLikeDislike(likeDislikeId: string): Observable<LikeDislike> {
    return this.http.delete<LikeDislike>(`${this.baseUrl}likes/${likeDislikeId}`);
  }

  getLikesDislikesByUserId(userId: string): Observable<LikeDislike[]> {
    return this.http.get<LikeDislike[]>(`${this.baseUrl}likes/users/${userId}`);
  }

  getLikesDislikesByCommentId(commentId: string): Observable<LikeDislike[]> {
    return this.http.get<LikeDislike[]>(`${this.baseUrl}likes/comments/${commentId}`);
  }

  getLikesCount(commentId: string): Observable<number> {
    return this.http.get<number>(`${this.baseUrl}likes/comments/${commentId}/likes`);
  }

  getDislikesCount(commentId: string): Observable<number> {
    return this.http.get<number>(`${this.baseUrl}likes/comments/${commentId}/dislikes`);
  }
}
