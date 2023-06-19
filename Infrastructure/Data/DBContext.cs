using System;
using System.Collections.Generic;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

public class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options) : base(options)
    {
    }

    // DbSet for generic entity
    public DbSet<Product> Product { get; set; }
    public DbSet<ClothingType> ClothingTypes { get; set; }
    public DbSet<LadiesType> LadiesTypes { get; set; }
    public DbSet<GentsType> GentsTypes { get; set; }
    public DbSet<SeasonType> SeasonTypes { get; set; }
    public DbSet<Clothing> Clothings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LadiesType>()
            .HasOne(lt => lt.ClothingType)
            .WithMany(ct => ct.LadiesTypes)
            .HasForeignKey(lt => lt.TypeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<GentsType>()
            .HasOne(gt => gt.ClothingType)
            .WithMany(ct => ct.GentsTypes)
            .HasForeignKey(gt => gt.TypeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Clothing>()
            .HasOne(c => c.ClothingType)
            .WithMany()
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Clothing>()
            .HasOne(c => c.SeasonType)
            .WithMany(st => st.Clothings)
            .HasForeignKey(c => c.SeasonId)
            .OnDelete(DeleteBehavior.Restrict);
    }


    // ... your DbSet and other configurations
}
