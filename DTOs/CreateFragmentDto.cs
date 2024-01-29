using LfragmentApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace LfragmentApi.DTOs
{
    public class CreateFragmentDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }

        public List<Tag> Tags { get; set; }
    }
}
