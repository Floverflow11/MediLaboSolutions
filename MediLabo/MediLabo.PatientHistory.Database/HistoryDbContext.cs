using MediLabo.PatientHistory.Domain;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace MediLabo.PatientHistory.Database;

public class HistoryDbContext : DbContext
{
    public DbSet<Note> Notes { get; set; }
    
    public HistoryDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Note>().ToCollection("Notes");
    }
}