export enum CardResult {
    Tie = 1,
    Winner = 2,
    Loser = 3
}

export enum DealVerdict {
    Tie = 1,
    LeftWin = 2,
    RightWin = 3
}

export type DealResponse = {
    leftCard: GameCard;
    rightCard: GameCard;
    verdict: DealVerdict;
};

export type GameCard = {
    name: string;
    result: CardResult;
    properties: CardProperty[];
};

export type CardProperty = {
    name: string;
    value: number;
    selected: boolean;
};

export type CardDefinition = {
    key: string;
    name: string;
    properties: string[];
};

export type Score = {
    leftScore: number;
    rightScore: number;
};
