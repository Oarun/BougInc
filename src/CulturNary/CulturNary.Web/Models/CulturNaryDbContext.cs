using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CulturNary.Web.Models;

public partial class CulturNaryDbContext : DbContext
{
    public CulturNaryDbContext()
    {
    }

    public CulturNaryDbContext(DbContextOptions<CulturNaryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Collection> Collections { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=CulturNaryDbContextConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Collection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Collecti__3213E83FE7ACFF34");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Img)
                .IsUnicode(false)
                .HasColumnName("img");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.Tags)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("tags");

            entity.HasOne(d => d.Person).WithMany(p => p.Collections)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK__Collectio__perso__693CA210");

            entity.HasMany(c => c.Recipes)
                .WithOne(r => r.Collection)
                .HasForeignKey(r => r.CollectionId)
                .OnDelete(DeleteBehavior.Cascade); // Configure cascade delete 
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Person__3213E83F48245AE2");

            entity.ToTable("Person");

            entity.HasIndex(e => e.IdentityId, "UQ__Person__D51AF5F52533064D").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdentityId).HasColumnName("identity_id");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Recipes__3213E83F69F77DDB");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CollectionId).HasColumnName("collection_id");
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Img)
                .IsUnicode(false)
                .HasColumnName("img");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.Uri)
                .IsUnicode(false)
                .HasColumnName("uri");

            entity.HasOne(d => d.Collection).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.CollectionId)
                .HasConstraintName("FK__Recipes__collect__6C190EBB");

            entity.HasOne(d => d.Person).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK__Recipes__person___6D0D32F4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
