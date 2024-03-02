using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Protocol.Plugins;
namespace CulturNary.Web.Models.DTO
{
   public class PersonDto
    {
        public int Id { get; set; }
        public string? IdentityId { get; set; }
    }
}