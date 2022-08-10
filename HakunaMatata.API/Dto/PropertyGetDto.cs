using HakunaMatata.Core.Models;

namespace HakunaMatata.API.Dto
{
    public class PropertyGetDto
    {
        public int PropertyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxGuests { get; set; }
        public string Address { get; set; }
        public double Price { get; set; }
        public List<Image> Images { get; set; }
    }
}
