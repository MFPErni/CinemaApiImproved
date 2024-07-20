namespace CinemaAPI_Improved.Dtos
{
    public class AddMovieDto
    {
        public string Title { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public int DirectorId { get; set; }
        public int GenreId { get; set; }
    }
}