using HakunaMatata.Core.Models;

namespace HakunaMatata.API.Dto
{
    public class UserGetDto
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public PropertyGetDto Property { get; set; }
        public List<UserReservationDto> Reservations { get; set; }
    }
}
