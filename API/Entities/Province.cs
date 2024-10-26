using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
  [Table("Cities")]
    public class Province
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }

    }
}