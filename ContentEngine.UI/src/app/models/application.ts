import { list, object, serializable } from "serializr";
import { v4 as uuidv4 } from 'uuid';
import { Page } from "./page";

export class Application {
    @serializable _id: string;
    @serializable _name?: string;
    @serializable(list(object(Page))) _pages?: Array<Page>;

    public get id(): string {
        return this._id;
    }

    public get name(): string | undefined {
        return this._name
    }
    public set name(value: string | undefined) {
        this._name = value;
    }

    public get pages(): Array<Page> | undefined {
        return this._pages;
    }
    public set pages(value: Array<Page> | undefined) {
        this._pages = value;
    }

    constructor() {
        this._id = uuidv4();
    }
}