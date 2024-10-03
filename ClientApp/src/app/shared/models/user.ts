import { UserPhoto } from "./user-photo";

export interface User {
  id: string;
  email: string;
  username: string;
  firstName: string;
  lastName: string;
  photoUrl: string;
  gender: string;
  age: number;
  created: string;
  lastActive: string;

  token: string;
  roles: string[];
  userPhotos: UserPhoto[];
}
