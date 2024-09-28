using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DotNetsTask.Data.Models;

public partial class DotNetsTasksDbContext : DbContext
{
    public DotNetsTasksDbContext()
    {
    }

    public DotNetsTasksDbContext(DbContextOptions<DotNetsTasksDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<IssuedBook> IssuedBooks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MSI\\SQLEXPRESS;Initial Catalog=DotNetTaskDb;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Books__3DE0C207C5504148");

            entity.Property(e => e.Author)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Genre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ISBN");
            entity.Property(e => e.PublishedYear)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Subject).HasMaxLength(200);
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<IssuedBook>(entity =>
        {
            entity.HasKey(e => e.IssueId).HasName("PK__IssuedBo__6C861624BEC98B91");

            entity.Property(e => e.IssueId).HasColumnName("IssueID");
            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ExpectedReturnDate).HasMaxLength(200);
            entity.Property(e => e.MobileNo).HasMaxLength(200);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Book).WithMany(p => p.IssuedBooks)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__IssuedBoo__BookI__3B75D760");

            entity.HasOne(d => d.User).WithMany(p => p.IssuedBooks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__IssuedBoo__UserI__3C69FB99");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC788C02DE");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.AdharCard).HasMaxLength(200);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Location).HasMaxLength(200);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password).HasMaxLength(200);
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
