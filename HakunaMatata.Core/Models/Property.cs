using System.ComponentModel.DataAnnotations.Schema;

namespace HakunaMatata.Core.Models
{
    public class Property
    {
        public Property() 
        { 
            Images = new List<Image>();
        }
        public int PropertyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxGuests { get; set; }
        public string Address { get; set; }
        public double Price { get; set; }
        public List<Image> Images { get; set; }
    }
}
