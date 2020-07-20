export type NewScoreModel = {
    points: number,
    playerName: string
}

export type ScoreModel = NewScoreModel & {
    id: number,
    points: number,
    entryDate: Date,
    playerName: string,
    gameId: number
}
