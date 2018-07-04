export interface ILoginModel {
  firstName: string;
  middleName: string;
  lastName: string;
}

export class LoginModel implements ILoginModel {
  constructor(
    public firstName: string,
    public middleName: string,
    public lastName: string) {}
}
