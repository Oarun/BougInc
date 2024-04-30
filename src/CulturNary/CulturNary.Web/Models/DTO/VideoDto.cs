using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Protocol.Plugins;
namespace CulturNary.Web.Models.DTO
{
    public class VideoDto
    {
        public int? PersonId { get; set; }
        public string VideoName { get; set; } = null!;
        public string? VideoType { get; set; }
        public string? VideoLink { get; set; }
        public string? VideoNotes { get; set; }
    }
}