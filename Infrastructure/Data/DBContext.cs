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

    // ... your DbSet and other configurations
}
