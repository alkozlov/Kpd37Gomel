export interface ILoginModel {
  firstName: string;
  middleName: string;
  lastName: string;
  apartmentNumber: string;
}

export class LoginModel implements ILoginModel {
  constructor(
    public firstName: string,
    public middleName: string,
    public lastName: string,
    public apartmentNumber: string) { }
}
