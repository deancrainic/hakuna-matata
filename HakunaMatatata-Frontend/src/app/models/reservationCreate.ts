export interface IReservationCreate {
    propertyId: number,
    checkinDate: string | null,
    checkoutDate: string | null,
    guestsNumber: number
}