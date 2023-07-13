using System;
using System.Collections.Generic;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Models;

public partial class InventoryContext : DbContext
{
    public InventoryContext()
    {
    }

    public InventoryContext(DbContextOptions<InventoryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Clothing> Clothings { get; set; }

    public virtual DbSet<ClothingType> ClothingTypes { get; set; }

    public virtual DbSet<Fabric> Fabrics { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Sales> Sales { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:moonclothhouse.database.windows.net,1433;Initial Catalog=Inventory;Persist Security Info=False;User ID=zeeshan;Password=Pakistan@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Clothing>(entity =>
        {
            entity.HasKey(e => e.ClothingId).HasName("PK__Clothing__7D52820F4D58FFFC");

            entity.ToTable("Clothing");

            entity.Property(e => e.ClothingId)
                .ValueGeneratedNever()
                .HasColumnName("clothing_id");
            entity.Property(e => e.ClothingName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("clothing_name");
            entity.Property(e => e.FabricId).HasColumnName("fabric_id");
            entity.Property(e => e.GenderId).HasColumnName("gender_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Fabric).WithMany(p => p.Clothings)
                .HasForeignKey(d => d.FabricId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Clothing__fabric__6477ECF3");

            entity.HasOne(d => d.Gender).WithMany(p => p.Clothings)
                .HasForeignKey(d => d.GenderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Clothing__gender__628FA481");

            entity.HasOne(d => d.Type).WithMany(p => p.Clothings)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Clothing__type_i__6383C8BA");
        });

        modelBuilder.Entity<ClothingType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__Clothing__2C0005984A4CFDCE");

            entity.ToTable("ClothingType");

            entity.Property(e => e.TypeId)
                .ValueGeneratedNever()
                .HasColumnName("type_id");
            entity.Property(e => e.TypeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<Fabric>(entity =>
        {
            entity.HasKey(e => e.FabricId).HasName("PK__Fabric__5BFCADFC57F88943");

            entity.ToTable("Fabric");

            entity.Property(e => e.FabricId)
                .ValueGeneratedNever()
                .HasColumnName("fabric_id");
            entity.Property(e => e.FabricName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("fabric_name");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.GenderId).HasName("PK__Gender__9DF18F87C0843C66");

            entity.ToTable("Gender");

            entity.Property(e => e.GenderId)
                .ValueGeneratedNever()
                .HasColumnName("gender_id");
            entity.Property(e => e.GenderName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("gender_name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<Sales>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PK__Sales__E1EB00B25AEBFD7B");

            entity.Property(e => e.SaleId).HasColumnName("sale_id");
            entity.Property(e => e.ActualPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("actual_price");
            entity.Property(e => e.ClothingId).HasColumnName("clothing_id");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.GenderId).HasColumnName("gender_id");
            entity.Property(e => e.SalePrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("sale_price");

            entity.HasOne(d => d.Clothing).WithMany(p => p.Sales)
                .HasForeignKey(d => d.ClothingId)
                .HasConstraintName("FK__Sales__clothing___06CD04F7");



            entity.HasOne(d => d.Gender).WithMany(p => p.Sales)
                .HasForeignKey(d => d.GenderId)
                .HasConstraintName("FK__Sales__gender_id__07C12930");


        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C98935A1A");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Usercategory)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
