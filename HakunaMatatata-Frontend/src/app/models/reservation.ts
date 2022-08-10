import { IReservationProperty } from "./reservationProperty";

export interface IReservation {
    reservationId: number,
    property: IReservationProperty,
    checkinDate: Date,
    checkoutDate: Date,
    guestsNumber: number,
    totalPrice: number
}