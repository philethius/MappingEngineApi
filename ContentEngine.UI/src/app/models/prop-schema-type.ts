export type SerializerType = (value: any) => any;
export type DeserializerType = (value: any, done: Function, contextObj: any) => void;
export type PropSchemaType = {
  serializer: SerializerType;
  deserializer: DeserializerType;
  jsonname?: string;
}