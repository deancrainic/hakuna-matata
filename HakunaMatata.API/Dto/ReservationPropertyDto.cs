namespace HakunaMatata.API.Dto
{
    public class ReservationPropertyDto
    {
        public int PropertyId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int MaxGuests { get; set; }

        public double Price { get; set; }
    }
}
