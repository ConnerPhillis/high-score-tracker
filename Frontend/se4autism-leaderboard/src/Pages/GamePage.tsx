import {useParams} from "react-router";
import {Button} from "@material-ui/core";
import React from "react";
import ScoreTable, {ScoreListStore} from "../Components/ScoreTable";
import Reference from "../Utilities/Reference";
import AddScoreModal from "../Components/AddScoreModal";
import {createStyles, makeStyles} from "@material-ui/core/styles";

const useStyles = makeStyles(() =>
    createStyles({
        tableMargin: {
            margin: '5px 20px'
        },
        buttonMargin: {
            margin: '30px'
        }
    }),
);

export default function GamePage() {

    const classes = useStyles()

    const {gameId} = useParams()

    const integerGameId = parseInt(gameId)

    const showModal: Reference<boolean> = new Reference(false)

    return (
        <>
            <div className={classes.tableMargin}>
                <ScoreTable store={new ScoreListStore(integerGameId)}/>
            </div>
            <Button className={classes.buttonMargin}
                variant="contained"
                color="primary"
                onClick={() => {
                    console.log('should show modal: ', !showModal.value)
                    showModal.value = !showModal.value
                }}>
                Add Your Score
            </Button>
            <AddScoreModal
                gameId={integerGameId}
                showModal={showModal}
                onClose={() => {
                    showModal.value = false
                }}/>
        </>
    )

}

