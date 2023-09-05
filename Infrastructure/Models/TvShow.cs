namespace Infrastructure.Models
{
    public class TvShow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public DateTime Premiered { get; set; }
        public List<string> Genres { get; set; }
        public string Summary { get; set; }
    }
}
