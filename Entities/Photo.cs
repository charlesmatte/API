using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; } // URL of the photo
        public bool IsMain { get; set; } // Is this the main photo?
        public string PublicId { get; set; } // Public ID of the photo
        public AppUser AppUser { get; set; } // The user who owns this photo
        public int AppUserId { get; set; } // The ID of the user who owns this photo
    }
}