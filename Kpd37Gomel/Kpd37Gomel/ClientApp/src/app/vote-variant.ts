export interface IVoteVariant {
  Id: string;
  VoteId: string;
  Text: string;
}

export class VoteVariant implements IVoteVariant {
  constructor(
    public Id: string,
    public VoteId: string,
    public Text: string) {}
}
