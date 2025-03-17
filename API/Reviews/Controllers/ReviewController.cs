using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MonApi.API.Reviews.DTOs;
using MonApi.API.Reviews.Services;

namespace MonApi.API.Reviews.Controllers;

[ApiController]
[Route("reviews")]
[Authorize]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpPost]
    public async Task<ActionResult<ReturnReviewDto>> CreateReview(CreateReviewDto createReviewDto)
    {
        return Ok(await _reviewService.CreateReview(createReviewDto));
    }

    [HttpPut]
    public async Task<ActionResult<ReturnReviewDto>> UpdateReview(UpdateReviewDto updateReviewDto)
    {
        return Ok(await _reviewService.UpdateReview(updateReviewDto));
    }

    [Authorize(Roles = "Employee")]
    [HttpPost("delete")]
    public async Task<ActionResult<ReturnReviewDto>> DeleteReview(DeleteReviewDto deleteReviewDto)
    {
        return Ok(await _reviewService.DeleteReview(deleteReviewDto));
    }
}