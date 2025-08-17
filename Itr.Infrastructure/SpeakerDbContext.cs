using Microsoft.EntityFrameworkCore;
using NetAPI.Domain.Entities;
using NetAPI.Domain.Enums;

namespace NetAPI.Infrastructure;

public class SpeakerDbContext: DbContext
{
    public SpeakerDbContext(DbContextOptions<SpeakerDbContext> options) : base(options) { }

    public DbSet<Speaker> Speakers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Speaker>().HasData(
            new Speaker
            {
                Id = 1,
                Name = "Alice Johnson",
                Gender = 0,
                Email = "alice.johnson@example.com",
                Country = Country.Pakistan,
                Phone = "+92-300-1234567",
                Dob = new DateTime(1985, 3, 15),
                AcceptTerms = true
            },
            new Speaker
            {
                Id = 2,
                Name = "Brian Smith",
                Gender = 1,
                Email = "brian.smith@example.com",
                Country = Country.Denmark,
                Phone = "+45-20-123456",
                Dob = new DateTime(1990, 7, 22),
                AcceptTerms = true
            },
            new Speaker
            {
                Id = 3,
                Name = "Carlos Diaz",
                Gender = 1,
                Email = "carlos.diaz@example.com",
                Country = Country.Morocco,
                Phone = "+212-600-123456",
                Dob = new DateTime(1988, 12, 5),
                AcceptTerms = true
            },
            new Speaker
            {
                Id = 4,
                Name = "Diana Miller",
                Gender = 0,
                Email = "diana.miller@example.com",
                Country = Country.England,
                Phone = "+44-7700-900123",
                Dob = new DateTime(1992, 5, 9),
                AcceptTerms = true
            }
        );
    }

}