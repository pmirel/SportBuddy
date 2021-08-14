import { User } from "./user";

export class UserParams {
  gender: string;
  sport: string;
  pageNumber = 1;
  pageSize = 8;
  orderBy = 'lastActive';

  constructor(user: User) {
    this.gender = user.gender === "male" ? "male" : "female";
  }

}