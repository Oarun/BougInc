using System;
using System.Collections.Generic;

namespace CulturNary.Web.Models;

public partial class BlockedUser
{
    public int Id { get; set; }

    public string? BlockerIdentityId { get; set; }

    public string? BlockedIdentityId { get; set; }

    public DateTime BlockDate { get; set; }

    public virtual Person? BlockedIdentity { get; set; }

    public virtual Person? BlockerIdentity { get; set; }
}
