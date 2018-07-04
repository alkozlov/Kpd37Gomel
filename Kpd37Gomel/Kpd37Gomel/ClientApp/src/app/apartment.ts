import {ITenant} from "./tenant";

export interface IApartment {
  id: string;
  apartmentNumber: string;
  floorNumber: number;
  totalAreaSnb: number;
  totalArea: number;
  livingArea: number;
  voteRate: number;
  owner: ITenant;
  tenants: Array<ITenant>;
}

export class Apartment implements IApartment {
  constructor(
    public id: string,
    public apartmentNumber: string,
    public floorNumber: number,
    public totalAreaSnb: number,
    public totalArea: number,
    public livingArea: number,
    public voteRate: number,
    public owner: ITenant,
    public tenants: Array<ITenant>) {}
}
