namespace HakunaMatata.Data.DTOs
{
    public class ReservationGetForPropertyDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime CheckinDate { get; set; }
        public DateTime CheckoutDate { get; set; }
    }
}
