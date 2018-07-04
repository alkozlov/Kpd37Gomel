import { IApartment } from "./apartment";

export interface ITenant {
  id: string;
  firstName: string;
  middleName: string;
  lastName: string;
  isApartmentOwner: boolean;
}

export class Tenant implements ITenant {
  constructor(
    public id: string,
    public firstName: string,
    public middleName: string,
    public lastName: string,
    public isApartmentOwner: boolean) { }
}
