namespace CinemaAPI_Improved.Entities
{
    public class Ticket
    {
        public int Id { get; set; }

        public DateOnly PurchaseDate { get; set; }

        public Movie Mdetails { get; set; }

        public Viewer Vdetails { get; set; }
    }
}
