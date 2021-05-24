using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Diploma.Models;

namespace Diploma.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<Precedent> Precedents{ get; set; }
        public DbSet<Attribute> Attributes { get; set; }
        public DbSet<AttributeType> AttributeTypes { get; set; }
        public DbSet<Cluster> Clusters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-RBL4H75;Database=Diploma;Trusted_Connection=True;MultipleActiveResultSets=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attribute>()
                .HasOne(b => b.Cluster)
                .WithMany(x => x.Attributes)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Attribute>()
                .HasOne(b => b.Precedent)
                .WithMany(x => x.Attributes)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Attribute>()
                .HasOne(b => b.AttributeType);

            modelBuilder.Entity<Attribute>()
                .HasKey(b => b.Id)
                .HasName("PrimaryKey_AttributeId");

            modelBuilder.Entity<AttributeType>()
                .HasKey(b => b.Id)
                .HasName("PrimaryKey_AttributeTypeId");

            modelBuilder.Entity<Cluster>()
                .HasKey(b => b.Id)
                .HasName("PrimaryKey_ClusterId");

            modelBuilder.Entity<Precedent>()
                .HasKey(b => b.Id)
                .HasName("PrimaryKey_PrecedentId");
        }

    }
}
