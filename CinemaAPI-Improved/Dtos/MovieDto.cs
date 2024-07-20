namespace CinemaAPI_Improved.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public string DirectorName { get; set; }
        public string GenreType { get; set; }
    }
}
