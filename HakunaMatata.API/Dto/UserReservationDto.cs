namespace HakunaMatata.API.Dto
{
    public class UserReservationDto
    {
        public int ReservationId { get; set; }
        public ReservationPropertyDto Property { get; set; }
        public double TotalPrice { get; set; }
    }
}
