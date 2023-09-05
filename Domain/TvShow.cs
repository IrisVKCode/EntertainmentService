using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class TvShow
    {
        public TvShow()
        {
            
        }

        public TvShow(int id, string name, string language, DateTime premiered, List<string> genres, string summary)
        {
            Id = id;
            Name = name;
            Language = language;
            Premiered = premiered;
            Genres = string.Join(';', genres);
            Summary = summary;
        }

        public int Id { get; private set; }

        [StringLength(100)]
        public string Name { get; private set; }

        [StringLength(50)]
        public string Language { get; private set; }
        public DateTime Premiered { get; private set; }
        public string Genres { get; private set; }
        public string Summary { get; private set; }
    }
}