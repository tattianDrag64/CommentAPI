using CommentAPI.Entities;
using CommentAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : Controller
    {
        private readonly ICommentService _service;
        //private readonly ILogger<CommentsController> _logger;
        public CommentsController(ICommentService CommentService)
        {
            _service = CommentService;
        }

        [HttpGet]
        //[Route("index")]
        public async Task<IActionResult> GetAllAsync()
        {
            var comments = await _service.GetAllAsync();
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var comment = await _service.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CommentEntity comment)
        {
            if (comment == null)
            {
                return BadRequest("Comment cannot be null");
            }
            var createdComment = await _service.CreateAsync(comment);
            return Ok(comment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] CommentEntity comment)
        {
            if (comment == null || comment.ID != id)
            {
                return BadRequest("Comment doesnt exist");
            }
            var existingComment = await _service.GetByIdAsync(id);
            if (existingComment == null)
            {
                return NotFound();
            }
            await _service.UpdateAsync(comment);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserIdAsync(Guid userId)
        {
            var comments = await _service.GetByUserIdAsync(userId);
            if (comments == null || !comments.Any())
            {
                return NotFound();
            }
            return Ok(comments);
        }
    }
}
