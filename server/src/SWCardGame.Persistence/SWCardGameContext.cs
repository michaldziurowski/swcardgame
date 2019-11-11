using Microsoft.EntityFrameworkCore;
using SWCardGame.Persistence.Entities;

namespace SWCardGame.Persistence
{
    public class SWCardGameContext : DbContext
    {
        public SWCardGameContext()
        {
        }

        public SWCardGameContext(DbContextOptions<SWCardGameContext> options)
                    : base(options)
        {
        }

        public DbSet<CardDefinition> CardDefinitions { get; set; }
        public DbSet<Card> Cards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CardDefinition>(e =>
            {
                e.HasIndex(d => d.Key).IsUnique();
                e.Property(d => d.Key).IsRequired();
                e.Property(d => d.Name).IsRequired();
                e.Property(d => d.Properties).IsRequired();
            });

            modelBuilder.Entity<Card>(e =>
            {
                e.HasOne(c => c.Definition).WithMany(d => d.Cards);
                e.Property(c => c.Properties).HasColumnType("jsonb").IsRequired();
                e.Property(c => c.Name).IsRequired();
            });
        }
    }
}