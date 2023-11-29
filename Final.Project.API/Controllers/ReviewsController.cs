using Final.Project.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Final.Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewsManager _reviewsManager;

        public ReviewsController(IReviewsManager reviewsManager)
        {
            _reviewsManager = reviewsManager;
        }

        #region  Add Review 

        [HttpPost]
        [Route("AddReview")]
        public ActionResult AddReview(AddReviewDto review)
        {
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdFromToken is null)
            {
                return BadRequest("not logged in");
            }

            bool status =_reviewsManager.AddReviewToProduct(userIdFromToken,review);

            if(status)
            {
                return Ok(new
                {
                    message = "Review Added Successfully"
                });
            }
            else
            {
                return BadRequest(new
                {
                    message = "Review Added Successfully"
                });
            }
        }
        #endregion



        [HttpPost("Products/{productId}/AddReview")]
        public IActionResult AddReview(int productId, [FromBody] ReviewDto reviewDto)
        {
            try
            {
                var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                _reviewsManager.AddReview(userIdFromToken, productId, reviewDto.Comment, reviewDto.Rating);

                return Ok("Review added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the review.");
            }
        }

        #region Get All Reviews

        [HttpGet]
        [Route("Dashboard/Reviews")]
        public ActionResult<IEnumerable<ReviewReadDto>> GetAllReviews()
        {
            IEnumerable<ReviewReadDto> reviews = _reviewsManager.GetAllReviews();
            return Ok(reviews);
        }
        #endregion

        #region Delete review Dashboard 

        [HttpDelete]
        [Route("Dashboard/Reviews/Delete")]
        public ActionResult Delete(ReviewKeyDto reviewKey)
        {
            bool isDeleted = _reviewsManager.DeleteReview(reviewKey);

            return isDeleted ? NoContent() : BadRequest();
        }

        #endregion
    }
}
