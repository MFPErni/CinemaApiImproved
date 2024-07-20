using Swashbuckle.AspNetCore.Annotations;

namespace CinemaAPI_Improved.Dtos
{
    public class AddTicketDto
    {
        [SwaggerSchema(Format = "date", Description = "Purchase date in yyyy-MM-dd format")]
        public DateOnly PurchaseDate { get; set; }

        public int MovieId { get; set; }

        public int ViewerId { get; set; }
    }
}
