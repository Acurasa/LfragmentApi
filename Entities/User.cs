namespace LfragmentApi.Entities
{
    public class User
    {
        public Guid Id {  get; set; }
        public string Password_hash { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public DateTime Created {  get; set; }

    }
}
