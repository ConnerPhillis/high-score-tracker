import { TableCell, TableContainer, TableBody, Table, TableRow, TableHead, makeStyles } from "@material-ui/core"
import { observable } from "mobx"
import React from "react"
import {ScoreModel} from "../Models/ScoreModel"
import ScoreClient from "../Clients/ScoreClient"
import { observer } from "mobx-react"

export class ScoreListStore {

    @observable
    public scores: ScoreModel[] = []

    constructor(gameId: number){
        
        const client = new ScoreClient()
        client.GetScores(gameId)
            .then(r => this.scores = r.data)
    }
}

type ScoreListProps = {
    store: ScoreListStore
}

const ScoreTable = observer((props: ScoreListProps) => {

    return (

        <TableContainer>
            <Table>
                <TableHead>
                    <TableCell align="center">Position</TableCell>
                    <TableCell>Player</TableCell>
                    <TableCell align="right">Score</TableCell>
                    <TableCell align="right">Recorded Date</TableCell>
                </TableHead>
                <TableBody>
                    {props.store.scores.map((score, index) => (
                        <TableRow key={score.id}>
                            <TableCell align="center">{index + 1}</TableCell>
                            <TableCell>{score.playerName}</TableCell>
                            <TableCell align="right">{score.points}</TableCell>
                            <TableCell align="right">{new Date(score.entryDate).toLocaleString()}</TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>

    )
    
})

export default ScoreTable