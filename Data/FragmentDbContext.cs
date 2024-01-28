using LfragmentApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime;

namespace LfragmentApi.Data
{
    public class FragmentDbContext : DbContext
    {
        public FragmentDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Fragment> Fragments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        
        
    }
}