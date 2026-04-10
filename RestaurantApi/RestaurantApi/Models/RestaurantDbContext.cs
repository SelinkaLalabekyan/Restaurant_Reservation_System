using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RestaurantApi.Models;

public partial class RestaurantDbContext : DbContext
{
    public RestaurantDbContext()
    {
    }

    public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActiveReservation> ActiveReservations { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<RestaurantArea> RestaurantAreas { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=RestaurantDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActiveReservation>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ActiveReservations");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ReservationId).HasColumnName("ReservationID");
            entity.Property(e => e.TableId).HasColumnName("TableID");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B8A669310B");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.ReservationId).HasName("PK__Reservat__B7EE5F04E7DF77C8");

            entity.ToTable("Reservation", tb => tb.HasTrigger("trg_NoOverlap"));

            entity.HasIndex(e => e.ReservationDate, "idx_reservation_date");

            entity.Property(e => e.ReservationId).HasColumnName("ReservationID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.StaffId).HasColumnName("StaffID");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.TableId).HasColumnName("TableID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Reservati__Custo__534D60F1");

            entity.HasOne(d => d.Staff).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__Reservati__Staff__5535A963");

            entity.HasOne(d => d.Table).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.TableId)
                .HasConstraintName("FK__Reservati__Table__5441852A");
        });

        modelBuilder.Entity<RestaurantArea>(entity =>
        {
            entity.HasKey(e => e.AreaId).HasName("PK__Restaura__70B8202866CE8E11");

            entity.ToTable("RestaurantArea");

            entity.Property(e => e.AreaId).HasColumnName("AreaID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Staff__96D4AAF7FC37ED8B");

            entity.Property(e => e.StaffId).HasColumnName("StaffID");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Role).HasMaxLength(50);
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.TableId).HasName("PK__Table__7D5F018EC22D7251");

            entity.ToTable("Table");

            entity.Property(e => e.TableId).HasColumnName("TableID");
            entity.Property(e => e.AreaId).HasColumnName("AreaID");

            entity.HasOne(d => d.Area).WithMany(p => p.Tables)
                .HasForeignKey(d => d.AreaId)
                .HasConstraintName("FK__Table__AreaID__4CA06362");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
