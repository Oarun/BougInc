using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Protocol.Plugins;
namespace CulturNary.Web.Models.DTO
{
    public class RestaurantDto
    {
        public int? PersonId { get; set; }

        public string RestaurantsName { get; set; } = null!;

        public string? RestaurantsAddress { get; set; }

        public string? RestaurantsWebsite { get; set; }

        public string? RestaurantsMenu { get; set; }

        public string? RestaurantsPhoneNumber { get; set; }

        public string? RestaurantsNotes { get; set; }

        public string? RestaurantType { get; set; }

    }
}