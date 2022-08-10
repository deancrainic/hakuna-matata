using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HakunaMatata.Core.Models
{
    public class User
    {
        public User()
        {
            Reservations = new List<Reservation>();
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Property? Property { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}