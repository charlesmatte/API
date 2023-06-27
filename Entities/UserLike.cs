namespace API.Entities
{
    public class UserLike
    {
        public AppUser SourceUser { get; set; } // user who likes another user
        public int SourceUserId { get; set; }
        public AppUser LikedUser { get; set; } // user who is liked by another user
        public int LikedUserId { get; set; }
    }
}