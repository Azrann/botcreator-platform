using Mentor.telegram.bot.models;
using Microsoft.EntityFrameworkCore;

namespace Mentor.telegram.bot.boundedContext
{
    public class BotDbContext : DbContext
    {
        public BotDbContext(DbContextOptions<BotDbContext> options)
            : base(options)
        {
        }

        public DbSet<EventItem> EventItems { get; set; }
        public DbSet<ChatLiveUser> ChatLiveUsers { get; set; }
        public DbSet<WebMetric> WebMetrics { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventItem>().Property(x => x.BotId).HasMaxLength(64);
            modelBuilder.Entity<EventItem>().Property(x => x.Name).HasMaxLength(35);

            modelBuilder.Entity<ChatLiveUser>().Property(x => x.BotId).HasMaxLength(64);
            modelBuilder.Entity<ChatLiveUser>().Property(x => x.Auth).HasMaxLength(32);
            modelBuilder.Entity<ChatLiveUser>().Property(x => x.Email).HasMaxLength(35);
            modelBuilder.Entity<ChatLiveUser>().Property(x => x.Name).HasMaxLength(35);
            modelBuilder.Entity<ChatLiveUser>().Property(x => x.ChannelId).HasMaxLength(35);


            modelBuilder.Entity<WebMetric>().Property(x => x.BotId).HasMaxLength(64);
            modelBuilder.Entity<WebMetric>().Property(x => x.UserAgent).HasMaxLength(256);
            modelBuilder.Entity<WebMetric>().Property(x => x.Language).HasMaxLength(8);
            modelBuilder.Entity<WebMetric>().Property(x => x.Platform).HasMaxLength(8);
            modelBuilder.Entity<WebMetric>().Property(x => x.BrowserLanguage).HasMaxLength(256);
            modelBuilder.Entity<WebMetric>().Property(x => x.Connection).HasMaxLength(256);
            modelBuilder.Entity<WebMetric>().Property(x => x.Cookies).HasMaxLength(5);
            modelBuilder.Entity<WebMetric>().Property(x => x.Webdriver).HasMaxLength(256);
            modelBuilder.Entity<WebMetric>().Property(x => x.Ip).HasMaxLength(16);
            modelBuilder.Entity<WebMetric>().Property(x => x.City).HasMaxLength(16);
            modelBuilder.Entity<WebMetric>().Property(x => x.Region).HasMaxLength(16);
            modelBuilder.Entity<WebMetric>().Property(x => x.Country).HasMaxLength(16);
            modelBuilder.Entity<WebMetric>().Property(x => x.Loc).HasMaxLength(32);
            modelBuilder.Entity<WebMetric>().Property(x => x.Postal).HasMaxLength(32);
            modelBuilder.Entity<WebMetric>().Property(x => x.Referrer).HasMaxLength(256);

            modelBuilder.Entity<Feedback>().Property(x => x.BotId).HasMaxLength(64);
            modelBuilder.Entity<Feedback>().Property(x => x.UserAgent).HasMaxLength(256);
            modelBuilder.Entity<Feedback>().Property(x => x.Email).HasMaxLength(32);
            modelBuilder.Entity<Feedback>().Property(x => x.Platform).HasMaxLength(8);
            base.OnModelCreating(modelBuilder);               
        }
    }
}
