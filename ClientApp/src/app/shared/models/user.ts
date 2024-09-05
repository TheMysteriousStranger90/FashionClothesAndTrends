export interface User {
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
}
