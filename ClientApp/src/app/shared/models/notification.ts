import { Guid } from "guid-typescript";

export interface Notification {
  id: Guid;
  text: string;
  isRead: boolean;
  createdAt: Date;
}
