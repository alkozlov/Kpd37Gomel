export interface ITenant {
  id: string;
  firstName: string;
  middleName: string;
  lastName: string;
  passportSeries: string;
  passportId: string;
  isApartmentOwner: boolean;
}

export class Tenant implements ITenant {
  constructor(
    public id: string,
    public firstName: string,
    public middleName: string,
    public lastName: string,
    public passportSeries: string,
    public passportId: string,
    public isApartmentOwner: boolean) {}
}
