namespace CinemaAPI_Improved.Dtos
{
    public class UpdateMovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public int DirectorId { get; set; }
        public int GenreId { get; set; }
    }
}
