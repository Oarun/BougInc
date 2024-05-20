using System;
using System.Collections.Generic;

namespace CulturNary.Web.Models;

public partial class Friendship
{
    public int Id { get; set; }

    public int? Person1Id { get; set; }

    public int? Person2Id { get; set; }

    public DateTime? FriendshipDate { get; set; }

    public virtual Person? Person1 { get; set; }

    public virtual Person? Person2 { get; set; }
}
