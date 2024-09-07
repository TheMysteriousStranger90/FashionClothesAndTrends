import { User } from "./user";

export interface LikeDislike {
  isLike: boolean;
  commentId: string;
  comment: Comment;
  userId: string;
  user: User;
}
