using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LfragmentApi.Entities
{
    public class Tag
    {
        [Key]
        public string Name { get; set; }
        public string Color { get; set; }
        [Column("fragment")]
        public Guid FragmentId {  get; set; }
    }
}
