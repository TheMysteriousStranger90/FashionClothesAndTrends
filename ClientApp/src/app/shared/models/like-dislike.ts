import { Guid } from "guid-typescript";
import { User } from "./user";

export interface LikeDislike {
  isLike: boolean;
  commentId: Guid;
  comment: Comment;
  userId: string;
  user: User;
}
