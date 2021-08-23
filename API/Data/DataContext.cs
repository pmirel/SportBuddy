using System.ComponentModel.DataAnnotations.Schema;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  [Table("Photos")]
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<AppUser> Users { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Challenge> Challenges { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Message>()
      .HasOne(u => u.Recipient)
      .WithMany(m => m.MessagesReceived)
      .OnDelete(DeleteBehavior.Restrict);

      builder.Entity<Message>()
      .HasOne(u => u.Sender)
      .WithMany(m => m.MessagesSent)
      .OnDelete(DeleteBehavior.Restrict);

      builder.Entity<Challenge>()
      .HasOne(u => u.Recipient)
      .WithMany(c => c.ChallengesReceived)
      .OnDelete(DeleteBehavior.Restrict);

      builder.Entity<Challenge>()
      .HasOne(u => u.Sender)
      .WithMany(c => c.ChallengesSent)
      .OnDelete(DeleteBehavior.Restrict);


    }

  }
}