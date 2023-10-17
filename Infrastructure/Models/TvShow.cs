using Newtonsoft.Json;

namespace Infrastructure.Models
{
    public class TvShow
    {
        [JsonProperty("Id")]
        public int ExternalId { get; set; }
        public string? Name { get; set; }
        public string? Language { get; set; }
        public DateTime? Premiered { get; set; }
        public List<string>? Genres { get; set; }
        public string? Summary { get; set; }
    }
}
