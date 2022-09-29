import { list, object, serializable } from "serializr";
import { LayoutSection } from "./layout-section";

export class Layout {
    public get isConfigured(): boolean {
        return this.name != null
            && this.zones != null && this.zones.length > 0
            && this.defaultZone != null;
    }

    @serializable name?: string;
    @serializable(list(object(LayoutSection))) zones?: Array<LayoutSection>;
    @serializable(object(LayoutSection)) defaultZone?: LayoutSection;
}