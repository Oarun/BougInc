using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Protocol.Plugins;
namespace CulturNary.Web.Models.DTO
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public int? CollectionId { get; set; }
        public int? PersonId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? RecipeImg {get; set; }
        public string? Uri { get; set; }
    }
}