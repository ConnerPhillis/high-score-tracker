import React from 'react';
import { Theme, createStyles, makeStyles, useTheme } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import CardMedia from '@material-ui/core/CardMedia';
import Typography from '@material-ui/core/Typography';
import GameModel from '../Models/GameModel';
import {ScoreModel} from '../Models/ScoreModel';
import { observer } from 'mobx-react';
import ScoreClient from '../Clients/ScoreClient';
import { observable } from 'mobx';

import { Link } from "react-router-dom"

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        root: {
            display: 'flex',
            overflow: 'auto',
            margin: theme.spacing(2)
        },
        details: {
            display: 'flex',
            flex: 1,
            flexDirection: 'column',
        },
        leader: {
            textAlign: 'center',
            flexDirection: 'row'
        },
        content: {
            flex: '1 0 auto',
            textAlign: "left"
        },
        cover: {
            width: 151,
        },
        centeredColumn: {
            display: 'flex',
            height: '100%',
            alignItems: 'center',
            flexDirection: "column",
            
        }
    }),
);

type GameCardProps = {
    game: GameModel,
}


export default function GameCard(props: GameCardProps) {
    const classes = useStyles();

    return (
        <Link to={`${props.game.id}`} style={{textDecoration: 'none'}}>
            <Card className={classes.root}>
                <CardMedia
                className={classes.cover}
                image={`api/games/${props.game.id.toString()}/image`}
                title="Live from space album cover" />
                <div className={classes.details}>
                    <CardContent className={classes.content}>
                        <Typography component="h5" variant="h5">
                            {props.game.name}
                        </Typography>
                        <Typography variant="subtitle1" color="textSecondary">
                            {props.game.description}
                        </Typography>
                    </CardContent>
                </div>
                <div>
                    <Leaderboard pointType={props.game.pointType} store={new LeaderboardStore(props.game.id)}/>
                </div>
            </Card>
        </Link>
    );
}

type LeaderboardProps = {
    store: LeaderboardStore
    pointType: string
}


const Leaderboard = observer(( {store, pointType }: LeaderboardProps) => {

    const classes = useStyles()

    return (
        <CardContent className={classes.centeredColumn}>
        <Typography>Leaderboard</Typography>
        {store.leaders.length === 0 && (<Typography>No Scores Recorded</Typography>)}
        {store.leaders.map((v, i) => (
            <Typography key={i}>
                {`${positionIndicator(i + 1)}: ${v.playerName} with ${v.points} ${pointType}` }
            </Typography>
        ))}
    </CardContent>
    )

})

class LeaderboardStore {

    @observable
    public leaders: ScoreModel[] = []

    constructor(gameId: number ){
        
        const client = new ScoreClient()
        client.GetScores(gameId, 1, 2)
            .then(r => this.leaders = r.data)
    }


}

const positionIndicator = (position: number) => {

    if(position === 1)
        return "1st"

    if(position === 2 || position === 3)
        return `${position}nd`

    return `${position}th`

}