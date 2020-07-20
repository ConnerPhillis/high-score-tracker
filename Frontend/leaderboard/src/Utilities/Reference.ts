import { observable } from "mobx"

export default class Reference<T> {

    @observable
    value: T

    constructor(value: T) {
        this.value = value
    }

}