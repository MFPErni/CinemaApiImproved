namespace CinemaAPI_Improved.Dtos
{
    public class TicketDto
    {
        public int Id { get; set; }
        public DateOnly PurchaseDate { get; set; }
        public MovieDto Mdetails { get; set; }
        public ViewerDto Vdetails { get; set; }
    }
}
