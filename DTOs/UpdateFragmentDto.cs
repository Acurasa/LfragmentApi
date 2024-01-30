using LfragmentApi.Entities;

namespace LfragmentApi.DTOs
{
    public class UpdateFragmentDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Tags { get; set; }
    }
}