using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CulturNary.Web.Models;

public partial class CulturNaryContext : DbContext
{
    public CulturNaryContext()
    {
    }

    public CulturNaryContext(DbContextOptions<CulturNaryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Collection> Collections { get; set; }

    public virtual DbSet<FavoriteRecipe> FavoriteRecipes { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=CulturNaryDbContextConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Collection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Collecti__3213E83F08E7BCF6");

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
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Collectio__perso__72C60C4A");
        });

        modelBuilder.Entity<FavoriteRecipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Favorite__3213E83FB3112622");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FavoriteDate)
                .HasColumnType("datetime")
                .HasColumnName("favorite_date");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");
            entity.Property(e => e.Tags)
                .IsUnicode(false)
                .HasColumnName("tags");

            entity.HasOne(d => d.Person).WithMany(p => p.FavoriteRecipes)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FavoriteR__perso__797309D9");

            entity.HasOne(d => d.Recipe).WithMany(p => p.FavoriteRecipes)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FavoriteR__recip__7A672E12");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Person__3213E83F5774CC28");

            entity.ToTable("Person");

            entity.HasIndex(e => e.IdentityId, "UQ__Person__D51AF5F5A6B4BFCC").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdentityId).HasColumnName("identity_id");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Recipes__3213E83F6925A6AB");

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

            entity.HasOne(d => d.Collection).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.CollectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Recipes__collect__75A278F5");

            entity.HasOne(d => d.Person).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Recipes__person___76969D2E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
