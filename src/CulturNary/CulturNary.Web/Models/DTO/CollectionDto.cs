using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Protocol.Plugins;
namespace CulturNary.Web.Models.DTO
{
    public class CollectionDto
    {
        public int Id { get; set; }
        public int? PersonId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? CollectionImg {get; set;}
        public string? Tags { get; set; }
    }
}