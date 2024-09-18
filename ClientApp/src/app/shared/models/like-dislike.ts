import { User } from "./user";
import { Comment } from "./comment";

export interface LikeDislike {
  isLike: boolean;
  commentId: string;
  comment: Comment;
  userId: string;
  username: string;
}
