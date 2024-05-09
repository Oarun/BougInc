using System.ComponentModel.DataAnnotations;
using CulturNary.Web.Areas.Identity.Data;
namespace CulturNary.Web.Models;
public class FriendModel
{
    public List<SiteUser>? Friends { get; set; } = null;
    public List<SiteUser>? FriendRequests { get; set; } = null;
    public List<string>? FriendTags { get; set; } = null;
    public List<string>? RequestTags { get; set; } = null;
}