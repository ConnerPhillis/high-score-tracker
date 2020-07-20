import RequestBase from "./RequestBase";
import GameModel from "../Models/GameModel";
import { AxiosResponse } from "axios";
import {NewScoreModel, ScoreModel} from "../Models/ScoreModel";

export default class ScoreClient extends RequestBase {

    
    public async GetScores(gameId: number, pageId: number = 1, maxResults: number = 25): Promise<AxiosResponse<ScoreModel[]>> {
        return await this._api.get<ScoreModel[]>(`games/${gameId}/scores`, {
            params: {
                pageId,
                maxResults
            }
        })
    }

    public async addScore(gameId: number, scoreModel: NewScoreModel): Promise<AxiosResponse<ScoreModel>> {
        return await this._api.post<ScoreModel>(`games/${gameId}/scores`, scoreModel)
    }


}