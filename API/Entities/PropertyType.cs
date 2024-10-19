using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
  [Table("PropertyTypes")]
    public class PropertyType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
    }
}