import { Guid } from "guid-typescript";

export interface UserPhoto {
  id: Guid;
  url: string;
  isMain: boolean;
}
