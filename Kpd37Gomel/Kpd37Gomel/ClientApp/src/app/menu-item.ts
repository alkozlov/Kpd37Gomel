export interface IMenuItem {
  title: string;
  route: string;
  iconClassName: string;
}

export class MenuItem implements IMenuItem {
  constructor(
    public title: string,
    public route: string,
    public iconClassName: string) { }
}
