using HakunaMatata.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace HakunaMatata.API.Dto
{
    public class UserCreateDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
