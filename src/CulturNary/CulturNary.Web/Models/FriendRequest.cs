using System;
using System.Collections.Generic;

namespace CulturNary.Web.Models;

public partial class FriendRequest
{
    public int Id { get; set; }

    public int RequesterId { get; set; }

    public int RecipientId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime RequestDate { get; set; }

    public virtual Person Recipient { get; set; } = null!;

    public virtual Person Requester { get; set; } = null!;
}
