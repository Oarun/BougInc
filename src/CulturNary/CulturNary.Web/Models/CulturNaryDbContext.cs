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

    public virtual DbSet<CalorieLog> CalorieLogs { get; set; }

    public virtual DbSet<CalorieTracker> CalorieTrackers { get; set; }

    public virtual DbSet<Collection> Collections { get; set; }

    public virtual DbSet<FavoriteRecipe> FavoriteRecipes { get; set; }

    public virtual DbSet<FriendRequest> FriendRequests { get; set; }

    public virtual DbSet<Friendship> Friendships { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<Restaurant> Restaurants { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=CulturNaryDbContextConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CalorieLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CalorieL__3213E83FAC976721");

            entity.ToTable("CalorieLog");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CaloriesLogged).HasColumnName("calories_logged");
            entity.Property(e => e.LogDate).HasColumnName("log_date");
            entity.Property(e => e.PersonId).HasColumnName("person_id");

            entity.HasOne(d => d.Person).WithMany(p => p.CalorieLogs)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CalorieLo__perso__345EC57D");
        });

        modelBuilder.Entity<CalorieTracker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CalorieT__3213E83F87C9A6B2");

            entity.ToTable("CalorieTracker");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PersonCalories).HasColumnName("person_calories");
            entity.Property(e => e.PersonId).HasColumnName("person_id");

            entity.HasOne(d => d.Person).WithMany(p => p.CalorieTrackers)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CalorieTr__perso__318258D2");
        });

        modelBuilder.Entity<Collection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Collecti__3213E83FA6736DE1");

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
                .HasConstraintName("FK__Collectio__perso__373B3228");
        });

        modelBuilder.Entity<FavoriteRecipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Favorite__3213E83FC30C690A");

            entity.ToTable("FavoriteRecipe");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FavoriteDate)
                .HasColumnType("datetime")
                .HasColumnName("favorite_date");
            entity.Property(e => e.ImageUrl).IsUnicode(false);
            entity.Property(e => e.Label).IsUnicode(false);
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");
            entity.Property(e => e.Tags)
                .IsUnicode(false)
                .HasColumnName("tags");
            entity.Property(e => e.Uri).IsUnicode(false);

            entity.HasOne(d => d.Person).WithMany(p => p.FavoriteRecipes)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FavoriteR__perso__3DE82FB7");

            entity.HasOne(d => d.Recipe).WithMany(p => p.FavoriteRecipes)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK__FavoriteR__recip__3EDC53F0");
        });

        modelBuilder.Entity<FriendRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FriendRe__3214EC07D2C07329");

            entity.ToTable("FriendRequest");

            entity.Property(e => e.RequestDate).HasColumnType("datetime");
            entity.Property(e => e.ResponseDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Recipient).WithMany(p => p.FriendRequestRecipients)
                .HasForeignKey(d => d.RecipientId)
                .HasConstraintName("FK__FriendReq__Recip__477199F1");

            entity.HasOne(d => d.Requester).WithMany(p => p.FriendRequestRequesters)
                .HasForeignKey(d => d.RequesterId)
                .HasConstraintName("FK__FriendReq__Reque__4865BE2A");
        });

        modelBuilder.Entity<Friendship>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Friendsh__3214EC07CE1BEC83");

            entity.ToTable("Friendship");

            entity.Property(e => e.FriendshipDate).HasColumnType("datetime");

            entity.HasOne(d => d.Person1).WithMany(p => p.FriendshipPerson1s)
                .HasForeignKey(d => d.Person1Id)
                .HasConstraintName("FK__Friendshi__Perso__4B422AD5");

            entity.HasOne(d => d.Person2).WithMany(p => p.FriendshipPerson2s)
                .HasForeignKey(d => d.Person2Id)
                .HasConstraintName("FK__Friendshi__Perso__4C364F0E");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Person__3213E83F46ACF465");

            entity.ToTable("Person");

            entity.HasIndex(e => e.IdentityId, "UQ__Person__D51AF5F5A106C14A").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdentityId).HasColumnName("identity_id");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Recipes__3213E83F31F6EE5D");

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
                .HasConstraintName("FK__Recipes__collect__3A179ED3");

            entity.HasOne(d => d.Person).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK__Recipes__person___3B0BC30C");
        });

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Restaura__3213E83F1F031D33");

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
                .HasConstraintName("FK__Restauran__perso__44952D46");
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Videos__3213E83F51143A9A");

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
                .HasConstraintName("FK__Videos__person_i__41B8C09B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
