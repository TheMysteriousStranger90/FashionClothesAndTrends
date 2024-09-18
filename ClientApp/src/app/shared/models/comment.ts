import { LikeDislike } from "./like-dislike";

export interface Comment {
  id: string;
  text: string;
  username: string;
  userId: string;
  createdAt: Date;
  timeAgo?: string;
  likesCount?: number;
  dislikesCount?: number;
  likesDislikes: LikeDislike[];
}
