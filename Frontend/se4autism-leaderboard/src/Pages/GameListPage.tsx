import React, { useContext, createContext } from "react";
import {observable, computed} from "mobx"
import {observer } from "mobx-react"
import GameClient from "../Clients/GameClient";
import GameModel from "../Models/GameModel";
import GameCard from "../Components/GameCard";

class GameListStore {

    private _client = new GameClient()

    @observable
    private _games: GameModel[] | undefined

    @computed
    public get games(): GameModel[] {
        if(!this._games)
            this._client.GetGames().then(r => this._games = r.data)
        return this._games || []
    }
}

const GameListPage = () => {

    const gameListStore = new GameListStore()

    return (
        <div>
            <GameList store={gameListStore}></GameList>    
        </div>
    )

}

const controller = new GameListStore()

type GameListProps = {
    store: GameListStore
}

const GameList = observer((props: GameListProps) => {

    return (
        <div>
            {props.store.games.sort((v1, v2) => v1.name < v2.name ? -1 : 1).map((v, i) => (
                <GameCard key={i} game={v}/>
            ))}
        </div>
     )
})

export default GameListPage