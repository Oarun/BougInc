using Newtonsoft.Json;

namespace CulturNary.Web.Models
{
    public class NewsApiResponse
    {
        public string Status { get; set; }
        public int TotalResults { get; set; }

        public List<object> Articles { get; set; }
    }
}
