export interface IVoteVariant {
  id: string;
  voteId: string;
  text: string;
}

export class VoteVariant implements IVoteVariant {
  constructor(
    public id: string,
    public voteId: string,
    public text: string) {}
}
