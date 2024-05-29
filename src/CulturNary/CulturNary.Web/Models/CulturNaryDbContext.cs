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

    public virtual DbSet<CalorieLog> CalorieLogs { get; set; }

    public virtual DbSet<CalorieTracker> CalorieTrackers { get; set; }

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
            entity.HasKey(e => e.Id).HasName("PK__BlockedU__3213E83FD985C1CE");

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
                .HasConstraintName("FK__BlockedUs__block__7993056A");

            entity.HasOne(d => d.BlockerIdentity).WithMany(p => p.BlockedUserBlockerIdentities)
                .HasPrincipalKey(p => p.IdentityId)
                .HasForeignKey(d => d.BlockerIdentityId)
                .HasConstraintName("FK__BlockedUs__block__789EE131");
        });

        modelBuilder.Entity<CalorieLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CalorieL__3213E83F7767A6ED");

            entity.ToTable("CalorieLog");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CaloriesLogged).HasColumnName("calories_logged");
            entity.Property(e => e.LogDate).HasColumnName("log_date");
            entity.Property(e => e.PersonId).HasColumnName("person_id");

            entity.HasOne(d => d.Person).WithMany(p => p.CalorieLogs)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CalorieLo__perso__041093DD");
        });

        modelBuilder.Entity<CalorieTracker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CalorieT__3213E83F88BA2231");

            entity.ToTable("CalorieTracker");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PersonCalories).HasColumnName("person_calories");
            entity.Property(e => e.PersonId).HasColumnName("person_id");

            entity.HasOne(d => d.Person).WithMany(p => p.CalorieTrackers)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CalorieTr__perso__01342732");
        });

        modelBuilder.Entity<Collection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Collecti__3213E83FDF9D9D65");

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
                .HasConstraintName("FK__Collectio__perso__62AFA012");
        });

        modelBuilder.Entity<FavoriteRecipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Favorite__3213E83F12AB6790");

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
            entity.HasKey(e => e.Id).HasName("PK__FriendRe__3214EC07BABDA004");

            entity.ToTable("FriendRequest");

            entity.Property(e => e.RequestDate).HasColumnType("datetime");
            entity.Property(e => e.ResponseDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Recipient).WithMany(p => p.FriendRequestRecipients)
                .HasForeignKey(d => d.RecipientId)
                .HasConstraintName("FK__FriendReq__Recip__70FDBF69");

            entity.HasOne(d => d.Requester).WithMany(p => p.FriendRequestRequesters)
                .HasForeignKey(d => d.RequesterId)
                .HasConstraintName("FK__FriendReq__Reque__71F1E3A2");
        });

        modelBuilder.Entity<Friendship>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Friendsh__3214EC073BA37DB4");

            entity.ToTable("Friendship");

            entity.Property(e => e.FriendshipDate).HasColumnType("datetime");

            entity.HasOne(d => d.Person1).WithMany(p => p.FriendshipPerson1s)
                .HasForeignKey(d => d.Person1Id)
                .HasConstraintName("FK__Friendshi__Perso__74CE504D");

            entity.HasOne(d => d.Person2).WithMany(p => p.FriendshipPerson2s)
                .HasForeignKey(d => d.Person2Id)
                .HasConstraintName("FK__Friendshi__Perso__75C27486");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Person__3213E83FE110CD2F");

            entity.ToTable("Person");

            entity.HasIndex(e => e.IdentityId, "UQ__Person__D51AF5F550844344").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdentityId).HasColumnName("identity_id");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Recipes__3213E83F460C87FF");

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
                .HasConstraintName("FK__Recipes__collect__658C0CBD");

            entity.HasOne(d => d.Person).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK__Recipes__person___668030F6");
        });

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Restaura__3213E83F5087DBED");

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
                .HasConstraintName("FK__Restauran__perso__6E2152BE");
        });

        modelBuilder.Entity<SharedRecipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SharedRe__3213E83F4101E0D4");

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
                .HasConstraintName("FK__SharedRec__favor__7E57BA87");

            entity.HasOne(d => d.SharedWith).WithMany(p => p.SharedRecipeSharedWiths)
                .HasForeignKey(d => d.SharedWithId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SharedRec__share__7D63964E");

            entity.HasOne(d => d.Sharer).WithMany(p => p.SharedRecipeSharers)
                .HasForeignKey(d => d.SharerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SharedRec__share__7C6F7215");
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Videos__3213E83F9963EE0E");

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
                .HasConstraintName("FK__Videos__person_i__6B44E613");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
