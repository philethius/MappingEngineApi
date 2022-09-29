import { object, serializable } from "serializr";
import { Layout } from "./layout";

export class Page {
    @serializable(object(Layout)) _layout?: Array<Layout>;

    public get layout(): Array<Layout> | undefined {
        return this._layout;
    }
    public set layout(value: Array<Layout> | undefined) {
        this._layout = value;
    }
}