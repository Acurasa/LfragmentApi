namespace LfragmentApi.Entities
{
    public class Fragment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; } 
        public DateTime Updated { get; set; } 
        public List<string> Tags { get; set; }
    }
}