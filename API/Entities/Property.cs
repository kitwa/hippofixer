
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Properties")]
    public class Property
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int PropertyManagerId { get; set; }
        public AppUser PropertyManager { get; set; }
        public ICollection<Unit> Units { get; set; }
        public bool Deleted { get; set; }

    }
} 