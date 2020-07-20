import axios, {AxiosInstance } from "axios"

export default class RequestBase {

    protected _api: AxiosInstance
    constructor() {
        this._api = axios.create({
			baseURL: `${window.location.origin}/api/`,
		})
    }


}