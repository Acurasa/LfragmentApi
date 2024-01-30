using Microsoft.AspNetCore.Identity;

namespace LfragmentApi.Entities
{
    public class User : IdentityUser<Guid>
    {
        public DateTime Created {  get; set; } 

    }
}
