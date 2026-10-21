using Mentor.telegram.bot.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mentor.telegram.bot.boundedContext
{
    public class MentorDbContext : DbContext
    {
        public MentorDbContext(DbContextOptions<MentorDbContext> options)
            : base(options)
        {
        }

        public DbSet<models.Mentor> Mentors { get; set; }
        public DbSet<models.Client> Clients { get; set; }
        public DbSet<PaymentInfo> PaymentInfos { get; set; }
        public DbSet<PaymentInfo> Subscriptions { get; set; }
        public DbSet<PaymentInfo> TelegramUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subscription>().Property(x => x.Fee).HasPrecision(15, 8);
            modelBuilder.Entity<Subscription>().Property(x => x.Currency).HasMaxLength(5);
            modelBuilder.Entity<TelegramUser>().Property(x => x.Id).HasMaxLength(35);
            modelBuilder.Entity<TelegramUser>().Property(x => x.FirstName).HasMaxLength(25);
            modelBuilder.Entity<TelegramUser>().Property(x => x.LastName).HasMaxLength(25);
            modelBuilder.Entity<TelegramUser>().Property(x => x.UserName).HasMaxLength(25);
            modelBuilder.Entity<PaymentInfo>().Property(x => x.Fee).HasPrecision(15, 8);
            modelBuilder.Entity<PaymentInfo>().Property(x => x.BuyerAmount).HasPrecision(15, 8);
            modelBuilder.Entity<PaymentInfo>().Property(x => x.OriginalAmount).HasPrecision(15, 8);
            modelBuilder.Entity<PaymentInfo>().Property(x => x.ReceivedAmount).HasPrecision(15, 8);
            modelBuilder.Entity<PaymentInfo>().Property(x => x.BuyerCurrency).HasMaxLength(5);
            modelBuilder.Entity<PaymentInfo>().Property(x => x.OriginalCurrency).HasMaxLength(5);
            modelBuilder.Entity<PaymentInfo>().Property(x => x.WalletAddress).HasMaxLength(35);
            base.OnModelCreating(modelBuilder);
        }
    }

    public static class SqlServerModelBuilderExtensions
    {
        public static PropertyBuilder<decimal?> HasPrecision(this PropertyBuilder<decimal?> builder, int precision, int scale)
        {
            return builder.HasColumnType($"decimal({precision},{scale})");
        }

        public static PropertyBuilder<decimal> HasPrecision(this PropertyBuilder<decimal> builder, int precision, int scale)
        {
            return builder.HasColumnType($"decimal({precision},{scale})");
        }
    }
}
