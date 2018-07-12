import { ITenant } from "./tenant";
import { IVoteVariant } from "./vote-variant";

export interface IVote {
  Id: string;
  Author: ITenant;
  CreateDateUtc: number;
  Title: string;
  Description: string;
  UseVoteRate: boolean;
  IsDeleted: boolean;
  Variants: Array<IVoteVariant>;
}

export class Vote implements IVote {
  constructor(
    public Id: string,
    public Author: ITenant,
    public CreateDateUtc: number,
    public Title: string,
    public Description: string,
    public UseVoteRate: boolean,
    public IsDeleted: boolean,
    public Variants: Array<IVoteVariant>) {}
}
