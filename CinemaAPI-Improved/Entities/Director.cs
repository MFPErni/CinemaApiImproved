namespace CinemaAPI_Improved.Entities
{
    public class Director
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateOnly Birthdate { get; set; }
    }
}
