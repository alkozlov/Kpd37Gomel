import { ITenant } from "./tenant";
import { IVoteVariant } from "./vote-variant";

export interface IVote {
  id: string;
  author: ITenant;
  createDateUtc: number;
  title: string;
  description: string;
  useVoteRate: boolean;
  variants: Array<IVoteVariant>;
}

export class Vote implements IVote {
  constructor(
    public id: string,
    public author: ITenant,
    public createDateUtc: number,
    public title: string,
    public description: string,
    public useVoteRate: boolean,
    public variants: Array<IVoteVariant>) {}
}
