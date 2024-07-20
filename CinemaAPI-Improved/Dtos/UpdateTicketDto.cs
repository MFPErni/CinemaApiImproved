using Swashbuckle.AspNetCore.Annotations;

namespace CinemaAPI_Improved.Dtos
{
    public class UpdateTicketDto
    {
        [SwaggerSchema(Format = "date", Description = "Purchase date in yyyy-MM-dd format")]
        public int Id { get; set; }
        public DateOnly PurchaseDate { get; set; }
        public int MovieId { get; set; }
        public int ViewerId { get; set; }
    }
}
