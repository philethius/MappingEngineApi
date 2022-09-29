import { serializable } from "serializr";

export class LayoutSection {
    public get isConfigured(): boolean {
        return this.name != null;
    }

    @serializable name?: string;
}