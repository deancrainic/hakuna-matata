using System.ComponentModel.DataAnnotations;

namespace HakunaMatata.API.Dto
{
    public class UserCredentialsDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
