using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CompShop.Models;

public partial class CompShopContext : DbContext
{
    public CompShopContext()
    {
    }

    public CompShopContext(DbContextOptions<CompShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Component> Components { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<StoreComponent> StoreComponents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress; Database=CompShop; User=ИСП-32; Password=1234567890; Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(e => e.Model).HasName("PK__Componen__FB104C1271C07A5B");

            entity.Property(e => e.Model).HasMaxLength(50);
            entity.Property(e => e.ComponentName).HasMaxLength(100);
            entity.Property(e => e.Manufacturer).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK__Stores__3B82F0E1A1B3B1EB");

            entity.Property(e => e.StoreId)
                .ValueGeneratedNever()
                .HasColumnName("StoreID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.StoreName).HasMaxLength(100);
        });

        modelBuilder.Entity<StoreComponent>(entity =>
        {
            entity.HasKey(e => new { e.StoreId, e.Model }).HasName("PK__StoreCom__0433F420882EC3AF");

            entity.Property(e => e.StoreId).HasColumnName("StoreID");
            entity.Property(e => e.Model).HasMaxLength(50);

            entity.HasOne(d => d.ModelNavigation).WithMany(p => p.StoreComponents)
                .HasForeignKey(d => d.Model)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StoreComp__Model__29572725");

            entity.HasOne(d => d.Store).WithMany(p => p.StoreComponents)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StoreComp__Store__286302EC");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
