namespace HakunaMatata.API.Dto
{
    public class ReservationGetDto
    {
        public int ReservationId { get; set; }
        public ReservationPropertyDto Property { get; set; }
        public DateTime CheckinDate { get; set; }
        public DateTime CheckoutDate { get; set; }
        public int GuestsNumber { get; set; }
        public double TotalPrice { get; set; }
    }
}
