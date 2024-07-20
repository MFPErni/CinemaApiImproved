using System.ComponentModel.DataAnnotations;

namespace CinemaAPI_Improved.Entities
{
    public class Viewer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int ContactNumber { get; set; }

        public string Email { get; set; }
    }
}
