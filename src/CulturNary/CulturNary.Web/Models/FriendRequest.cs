using System;
using System.Collections.Generic;

namespace CulturNary.Web.Models;

public partial class FriendRequest
{
    public int Id { get; set; }

    public int? RequesterId { get; set; }

    public int? RecipientId { get; set; }

    public string? Status { get; set; }

    public DateTime? RequestDate { get; set; }

    public DateTime? ResponseDate { get; set; }

    public virtual Person? Recipient { get; set; }

    public virtual Person? Requester { get; set; }
}
