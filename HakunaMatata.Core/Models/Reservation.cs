namespace HakunaMatata.Core.Models
{
    public class Reservation
    {
        public Reservation() { }
        public int ReservationId { get; set; }
        public Property Property { get; set; }
        public DateTime CheckinDate { get; set; }
        public DateTime CheckoutDate { get; set; }
        public int GuestsNumber { get; set; }
        public double TotalPrice { get; set; }
    }
}
