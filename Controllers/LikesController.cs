using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class LikesController : BaseApiController
    {
        private readonly IUnitOfWork _uow;

        public LikesController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpPost("{username}")] // api/likes/username
        public async Task<ActionResult> AddLike(string username)
        {
            var sourceUserId = User.GetUserId(); // get the current user
            var likedUser = await _uow.UserRepository.GetUserByUsernameAsync(username); // get the user who is liked
            var sourceUser = await _uow.LikesRepository.GetUserWithLikes(sourceUserId); // get the current user with their likes

            if (likedUser == null) return NotFound(); // if the user who is liked is not found

            if (sourceUser.UserName == username) return BadRequest("You cannot like yourself"); // if the current user tries to like themselves
            var userLike = await _uow.LikesRepository.GetUserLike(sourceUserId, likedUser.Id); // check if the current user has liked the user who is liked
            if (userLike != null) return BadRequest("You already like this user"); // if the current user has already liked the user who is liked

            userLike = new UserLike // create a new user like
            {
                SourceUserId = sourceUserId,
                LikedUserId = likedUser.Id
            };

            sourceUser.LikedUsers.Add(userLike); // add the user like to the current user's liked users

            if (await _uow.Complete()) return Ok(); // if the user like is successfully added to the current user's liked users

            return BadRequest("Failed to like user"); // if the user like is not successfully added to the current user's liked users
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<LikeDto>>> GetUserLikes([FromQuery] LikesParams likesParams)
        {
            likesParams.UserId = User.GetUserId(); // get the current user
            var users = await _uow.LikesRepository.GetUserLikes(likesParams); // get all the users who are liked by the current user or who like the current user
            Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages)); // add pagination header to the response
            return Ok(users);
        }
    }
}