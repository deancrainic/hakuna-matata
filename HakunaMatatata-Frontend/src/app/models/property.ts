import { IImage } from "./image";

export interface IProperty {
    propertyId: number,
    name: string,
    description: string,
    address: string,
    maxGuests: number,
    price: number,
    images: IImage[]
}