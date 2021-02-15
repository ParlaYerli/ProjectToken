using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Web.API.Domain.Model;

#nullable disable

namespace Web.API.Domain.Entities
{
    public partial class ProjectTokenDBContext : DbContext
    {
        public ProjectTokenDBContext()
        {
        }
        public ProjectTokenDBContext(DbContextOptions<ProjectTokenDBContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
