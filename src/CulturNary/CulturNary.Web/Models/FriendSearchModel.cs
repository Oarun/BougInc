using System.ComponentModel.DataAnnotations;
using CulturNary.Web.Areas.Identity.Data;
namespace CulturNary.Web.Models;
public class FriendSearchModel
{
    public List<SiteUser>? Users { get; set; } = null;
    public bool IsSubmitted { get; set; } = false;
    public List<string>? Tags { get; set; } = null;
    public List<string>? FriendshipStatus { get; set; } = null;
    public Dictionary<SiteUser, double> UserMatchPercentages { get; set; } = null;
    public bool[] GetDietaryRestrictionsActiveArray()
    {
        var excludedProperties = new HashSet<string>
        {
            nameof(Users),
            nameof(IsSubmitted),
            nameof(FriendshipStatus),
            nameof(UserMatchPercentages)
        };
    
        var properties = GetType().GetProperties()
            .Where(p => p.PropertyType == typeof(bool) && !excludedProperties.Contains(p.Name));
    
        return properties.Select(p => (bool)p.GetValue(this)).ToArray();
    }
}