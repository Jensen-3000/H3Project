export interface User {
  id: number;
  username: string;
  email: string;
  role: {
    id: number;
    role: string;
  };
}
