namespace CinemaAPI_Improved.Entities
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateOnly ReleaseDate { get; set; }

        public Director DDetails { get; set; }

        public Genre GDetails { get; set; }
    }
}
