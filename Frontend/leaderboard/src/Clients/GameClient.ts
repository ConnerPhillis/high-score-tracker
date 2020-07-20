import RequestBase from "./RequestBase";
import GameModel from "../Models/GameModel";
import { AxiosResponse } from "axios";

export default class GameClient extends RequestBase{


    public async GetGames(): Promise<AxiosResponse<GameModel[]>> {
        return await this._api.get<GameModel[]>('games')
    }


}