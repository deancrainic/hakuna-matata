using HakunaMatata.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace HakunaMatata.API.Dto
{
    public class ReservationCreateDto
    {
        [Required]
        public int PropertyId { get; set; }
        [Required]
        public string CheckinDate { get; set; }
        [Required]
        public string CheckoutDate { get; set; }
        [Required]
        public int GuestsNumber { get; set; }
    }
}
