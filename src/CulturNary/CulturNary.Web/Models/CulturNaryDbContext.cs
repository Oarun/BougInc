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

    public virtual DbSet<FavoriteRecipe> FavoriteRecipes { get; set; }

    public virtual DbSet<FriendRequest> FriendRequests { get; set; }

    public virtual DbSet<Friendship> Friendships { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=CulturNaryDbContextConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Collection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Collecti__3213E83FD0FEBABB");

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
                .HasConstraintName("FK__Collectio__perso__57A801BA");
        });

        modelBuilder.Entity<FavoriteRecipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Favorite__3213E83F8B7F8019");

            entity.ToTable("FavoriteRecipe");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FavoriteDate)
                .HasColumnType("datetime")
                .HasColumnName("favorite_date");
            entity.Property(e => e.ImageUrl).IsUnicode(false);
            entity.Property(e => e.Label).IsUnicode(false);
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.RecipeId)
                .IsUnicode(false)
                .HasColumnName("recipe_id");
            entity.Property(e => e.Tags)
                .IsUnicode(false)
                .HasColumnName("tags");
            entity.Property(e => e.Uri).IsUnicode(false);
        });

        modelBuilder.Entity<FriendRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FriendRe__3213E83F814D0602");

            entity.ToTable("FriendRequest");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RecipientId).HasColumnName("recipient_id");
            entity.Property(e => e.RequestDate)
                .HasColumnType("datetime")
                .HasColumnName("request_date");
            entity.Property(e => e.RequesterId).HasColumnName("requester_id");
            entity.Property(e => e.ResponseDate)
                .HasColumnType("datetime")
                .HasColumnName("response_date");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");

            entity.HasOne(d => d.Recipient).WithMany(p => p.FriendRequestRecipients)
                .HasForeignKey(d => d.RecipientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FriendReq__recip__61316BF4");

            entity.HasOne(d => d.Requester).WithMany(p => p.FriendRequestRequesters)
                .HasForeignKey(d => d.RequesterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FriendReq__reque__603D47BB");
        });

        modelBuilder.Entity<Friendship>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Friendsh__3213E83F03A645D1");

            entity.ToTable("Friendship");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FriendshipDate)
                .HasColumnType("datetime")
                .HasColumnName("friendship_date");
            entity.Property(e => e.Person1Id).HasColumnName("person1_id");
            entity.Property(e => e.Person2Id).HasColumnName("person2_id");

            entity.HasOne(d => d.Person1).WithMany(p => p.FriendshipPerson1s)
                .HasForeignKey(d => d.Person1Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_person1");

            entity.HasOne(d => d.Person2).WithMany(p => p.FriendshipPerson2s)
                .HasForeignKey(d => d.Person2Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_person2");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Person__3213E83FAEB4DF3B");

            entity.ToTable("Person");

            entity.HasIndex(e => e.IdentityId, "UQ__Person__D51AF5F548C3684E").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdentityId).HasColumnName("identity_id");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Recipes__3213E83F460A2E83");

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
                .HasConstraintName("FK__Recipes__collect__5A846E65");

            entity.HasOne(d => d.Person).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK__Recipes__person___5B78929E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
