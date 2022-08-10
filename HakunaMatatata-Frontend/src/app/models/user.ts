import { IProperty } from "./property";
import { IReservation } from "./reservation";

export interface IUser {
    userId: number,
    email: string,
    firstName: string,
    lastName: string, 
    property: IProperty,
    reservations: IReservation[]
}