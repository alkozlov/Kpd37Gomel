export interface IUserData {
  fullName: string;
  apartmentNumber: string;
  isAdmin: boolean;
}

export class UserData implements IUserData {
  
  constructor(
    public fullName: string,
    public apartmentNumber: string,
    public isAdmin: boolean) { }

  public static getDefaultUser(): IUserData {
    return new UserData('', '', false);
  }
}
