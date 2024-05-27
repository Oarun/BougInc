using System;
using System.Collections.Generic;

namespace CulturNary.Web.Models;

public partial class Person
{
    public int Id { get; set; }

    public string IdentityId { get; set; } = null!;

    public virtual ICollection<BlockedUser> BlockedUserBlockedIdentities { get; set; } = new List<BlockedUser>();

    public virtual ICollection<BlockedUser> BlockedUserBlockerIdentities { get; set; } = new List<BlockedUser>();

    public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();

    public virtual ICollection<FriendRequest> FriendRequestRecipients { get; set; } = new List<FriendRequest>();

    public virtual ICollection<FriendRequest> FriendRequestRequesters { get; set; } = new List<FriendRequest>();

    public virtual ICollection<Friendship> FriendshipPerson1s { get; set; } = new List<Friendship>();

    public virtual ICollection<Friendship> FriendshipPerson2s { get; set; } = new List<Friendship>();

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

    public virtual ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();

    public virtual ICollection<SharedRecipe> SharedRecipeSharedWiths { get; set; } = new List<SharedRecipe>();

    public virtual ICollection<SharedRecipe> SharedRecipeSharers { get; set; } = new List<SharedRecipe>();

    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
}
