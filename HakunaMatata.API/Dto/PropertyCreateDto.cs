using HakunaMatata.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace HakunaMatata.API.Dto
{
    public class PropertyCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int MaxGuests { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
