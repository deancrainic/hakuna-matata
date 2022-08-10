using System.ComponentModel.DataAnnotations;

namespace HakunaMatata.API.Dto
{
    public class ReservationUpdateDto
    {
        [Required]
        public string CheckinDate { get; set; }
        [Required]
        public string CheckoutDate { get; set; }
        [Required]
        public int GuestsNumber { get; set; }
    }
}
