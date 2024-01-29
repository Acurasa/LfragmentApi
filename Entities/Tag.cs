using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LfragmentApi.Entities
{
    [Owned]
    public class Tag
    {
        [Key]
        public string Name { get; set; }
        public string Color { get; set; }
    }
}