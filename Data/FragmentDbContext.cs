using LfragmentApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime;

namespace LfragmentApi.Data
{
    public class FragmentDbContext : DbContext
    {
        public FragmentDbContext(DbContextOptions<FragmentDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Fragment>()
            //    .HasMany(f => f.Tags)
            //    .WithMany();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Fragment> Fragments { get; set; }
        //public DbSet<User> Users { get; set; }
    }
}