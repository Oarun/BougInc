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

    public virtual DbSet<BlockedUser> BlockedUsers { get; set; }

    public virtual DbSet<Collection> Collections { get; set; }

    public virtual DbSet<FavoriteRecipe> FavoriteRecipes { get; set; }

    public virtual DbSet<FriendRequest> FriendRequests { get; set; }

    public virtual DbSet<Friendship> Friendships { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<Restaurant> Restaurants { get; set; }

    public virtual DbSet<SharedRecipe> SharedRecipes { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=CulturNaryDbContextConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlockedUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BlockedU__3213E83F3AC5AF5E");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BlockDate)
                .HasColumnType("datetime")
                .HasColumnName("block_date");
            entity.Property(e => e.BlockedIdentityId)
                .HasMaxLength(450)
                .HasColumnName("blocked_identity_id");
            entity.Property(e => e.BlockerIdentityId)
                .HasMaxLength(450)
                .HasColumnName("blocker_identity_id");

            entity.HasOne(d => d.BlockedIdentity).WithMany(p => p.BlockedUserBlockedIdentities)
                .HasPrincipalKey(p => p.IdentityId)
                .HasForeignKey(d => d.BlockedIdentityId)
                .HasConstraintName("FK__BlockedUs__block__536D5C82");

            entity.HasOne(d => d.BlockerIdentity).WithMany(p => p.BlockedUserBlockerIdentities)
                .HasPrincipalKey(p => p.IdentityId)
                .HasForeignKey(d => d.BlockerIdentityId)
                .HasConstraintName("FK__BlockedUs__block__52793849");
        });

        modelBuilder.Entity<Collection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Collecti__3213E83FA4E7B4F8");

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
                .HasConstraintName("FK__Collectio__perso__3C89F72A");
        });

        modelBuilder.Entity<FavoriteRecipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Favorite__3213E83F038AAC01");

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
            entity.HasKey(e => e.Id).HasName("PK__FriendRe__3214EC0731DB9327");

            entity.ToTable("FriendRequest");

            entity.Property(e => e.RequestDate).HasColumnType("datetime");
            entity.Property(e => e.ResponseDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Recipient).WithMany(p => p.FriendRequestRecipients)
                .HasForeignKey(d => d.RecipientId)
                .HasConstraintName("FK__FriendReq__Recip__4AD81681");

            entity.HasOne(d => d.Requester).WithMany(p => p.FriendRequestRequesters)
                .HasForeignKey(d => d.RequesterId)
                .HasConstraintName("FK__FriendReq__Reque__4BCC3ABA");
        });

        modelBuilder.Entity<Friendship>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Friendsh__3214EC076A7C88C0");

            entity.ToTable("Friendship");

            entity.Property(e => e.FriendshipDate).HasColumnType("datetime");

            entity.HasOne(d => d.Person1).WithMany(p => p.FriendshipPerson1s)
                .HasForeignKey(d => d.Person1Id)
                .HasConstraintName("FK__Friendshi__Perso__4EA8A765");

            entity.HasOne(d => d.Person2).WithMany(p => p.FriendshipPerson2s)
                .HasForeignKey(d => d.Person2Id)
                .HasConstraintName("FK__Friendshi__Perso__4F9CCB9E");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Person__3213E83F19633182");

            entity.ToTable("Person");

            entity.HasIndex(e => e.IdentityId, "UQ__Person__D51AF5F5F8BC10D0").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdentityId).HasColumnName("identity_id");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Recipes__3213E83F7685A413");

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
                .HasConstraintName("FK__Recipes__collect__3F6663D5");

            entity.HasOne(d => d.Person).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK__Recipes__person___405A880E");
        });

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Restaura__3213E83FB1E81AA7");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.RestaurantType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("restaurant_type");
            entity.Property(e => e.RestaurantsAddress)
                .IsUnicode(false)
                .HasColumnName("restaurants_address");
            entity.Property(e => e.RestaurantsMenu)
                .IsUnicode(false)
                .HasColumnName("restaurants_menu");
            entity.Property(e => e.RestaurantsName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("restaurants_name");
            entity.Property(e => e.RestaurantsNotes)
                .IsUnicode(false)
                .HasColumnName("restaurants_notes");
            entity.Property(e => e.RestaurantsPhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("restaurants_phone_number");
            entity.Property(e => e.RestaurantsWebsite)
                .IsUnicode(false)
                .HasColumnName("restaurants_website");

            entity.HasOne(d => d.Person).WithMany(p => p.Restaurants)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK__Restauran__perso__47FBA9D6");
        });

        modelBuilder.Entity<SharedRecipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SharedRe__3213E83FF19EF3F9");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FavoriteRecipeId).HasColumnName("favorite_recipe_id");
            entity.Property(e => e.ShareDate)
                .HasColumnType("datetime")
                .HasColumnName("share_date");
            entity.Property(e => e.SharedWithId).HasColumnName("shared_with_id");
            entity.Property(e => e.SharerId).HasColumnName("sharer_id");

            entity.HasOne(d => d.FavoriteRecipe).WithMany(p => p.SharedRecipes)
                .HasForeignKey(d => d.FavoriteRecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SharedRec__favor__5832119F");

            entity.HasOne(d => d.SharedWith).WithMany(p => p.SharedRecipeSharedWiths)
                .HasForeignKey(d => d.SharedWithId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SharedRec__share__573DED66");

            entity.HasOne(d => d.Sharer).WithMany(p => p.SharedRecipeSharers)
                .HasForeignKey(d => d.SharerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SharedRec__share__5649C92D");
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Videos__3213E83F7E87562A");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.VideoLink)
                .IsUnicode(false)
                .HasColumnName("video_link");
            entity.Property(e => e.VideoName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("video_name");
            entity.Property(e => e.VideoNotes)
                .IsUnicode(false)
                .HasColumnName("video_notes");
            entity.Property(e => e.VideoType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("video_type");

            entity.HasOne(d => d.Person).WithMany(p => p.Videos)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK__Videos__person_i__451F3D2B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
