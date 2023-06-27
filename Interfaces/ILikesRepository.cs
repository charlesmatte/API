using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface ILikesRepository
    {
        Task<UserLike> GetUserLike(int sourceUserId, int likedUserId); // check if a user has liked another user
        Task<AppUser> GetUserWithLikes(int userId); // get a user with their likes
        Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams);
    }
}