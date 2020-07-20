import React, {ChangeEvent, ReactNode} from 'react'
import {makeStyles, Theme, createStyles} from '@material-ui/core/styles'
import {TextField, Button, Modal, Typography} from '@material-ui/core';
import {observer} from 'mobx-react';
import Reference from '../Utilities/Reference';
import ScoreClient from "../Clients/ScoreClient";

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        paper: {
            position: 'absolute',
            width: 400,
            backgroundColor: theme.palette.background.paper,
            border: '2px solid #000',
            boxShadow: theme.shadows[5],
            padding: theme.spacing(2, 4, 3),
        },
        internalStyle: {
            display: "flex",
            flexDirection: "column",
            padding: "5px"
        },
        childMargin: {
            margin: '5px'
        }
    }),
);

const getModalStyle = () => {
    const top = 50
    const left = 50

    return {
        top: `${top}%`,
        left: `${left}%`,
        transform: `translate(-${top}%, -${left}%)`,
    };
}

type AddScoreModalProps = {
    showModal: Reference<boolean>
    gameId: number
    onClose: () => void
}

const AddScoreModal = observer((props: AddScoreModalProps) => {

    const scoreClient = new ScoreClient()

    const classes = useStyles()

    console.log('rendered')

    const nameReference = new Reference<string>('');
    const pointReference = new Reference<string>('')

    const body = (
        <div style={getModalStyle()} className={classes.paper}>
            <div className={classes.internalStyle}>
                <Typography className={classes.childMargin}>
                    Add A New Score
                </Typography>
                <ObservableInput label="Your Name" value={nameReference}/>
                <ObservableInput label="Points" value={pointReference}/>
                <Button className={classes.childMargin} variant="contained" color="primary" onClick={() => {
                    scoreClient.addScore(props.gameId, {
                        playerName: nameReference.value,
                        points: parseInt(pointReference.value)
                    //    this is pretty gross, but it gets the job done for now
                    }).catch(v => alert('failed to upload score, somethings wrong!'))
                    window.location.reload()
                }}>Save Score</Button>
            </div>
        </div>
    )

    return (
        <Modal
            open={props.showModal.value}
            onClose={props.onClose}>
            {body}
        </Modal>
    )
})

type ObservableInputProps<T> = {
    label: string,
    value: Reference<T>
}

const ObservableInput = observer(<T extends ReactNode>(props: ObservableInputProps<T>) => {

    const classes = useStyles()

    return (<TextField
        className={classes.childMargin}
        label={props.label}
        value={props.value.value}
        onChange={(e: ChangeEvent<HTMLInputElement>) => {
            props.value.value = e.currentTarget.value as T
        }}/>)
})

export default AddScoreModal