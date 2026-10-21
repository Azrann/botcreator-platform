using Microsoft.EntityFrameworkCore;

namespace BotCreator.Infrastructure.Data.EfCore
{
    public partial class BotManagerContext : DbContext
    {
        public BotManagerContext()
        {
        }

        public BotManagerContext(DbContextOptions<BotManagerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bot> Bots { get; set; }
        public virtual DbSet<BotTemplate> BotTemplates { get; set; }
        public virtual DbSet<ResetToken> ResetTokens { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bot>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.BotTemplate)
                    .WithMany(p => p.Bots)
                    .HasForeignKey(d => d.BotTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bots_BotTemplates");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Bots)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK_Bots_Users");
            });

            modelBuilder.Entity<BotTemplate>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("IX_BotTemplates")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Avatar).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ResetToken>(entity =>
            {
                entity.HasIndex(e => e.Token)
                    .IsUnique();

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.Expires).HasColumnType("datetime");

                entity.Property(e => e.Issued).HasColumnType("datetime");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ResetTokens)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResetTokens_Users");
            });
        }
    }
}
