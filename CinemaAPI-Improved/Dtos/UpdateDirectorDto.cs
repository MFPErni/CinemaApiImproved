namespace CinemaAPI_Improved.Dtos
{
    public class UpdateDirectorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly Birthdate { get; set; }
    }
}
