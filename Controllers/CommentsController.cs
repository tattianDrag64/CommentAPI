using CommentAPI.Entities;
using CommentAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CommentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        //private readonly ILogger<CommentsController> _logger;
        public CommentsController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpGet]
        //[Route("index")]
        public async Task<IActionResult> GetAllAsync()
        {
            var comments = await _commentRepository.GetAllAsync();
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
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
            var createdComment = await _commentRepository.CreateAsync(comment);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = createdComment.ID }, createdComment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] CommentEntity comment)
        {
            if (comment == null || comment.ID != id)
            {
                return BadRequest("Comment doesnt exist");
            }
            var existingComment = await _commentRepository.GetByIdAsync(id);
            if (existingComment == null)
            {
                return NotFound();
            }
            await _commentRepository.UpdateAsync(comment);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            await _commentRepository.DeleteAsync(comment);
            return NoContent();
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserIdAsync(Guid userId)
        {
            var comments = await _commentRepository.GetByUserIdAsync(userId);
            if (comments == null || !comments.Any())
            {
                return NotFound();
            }
            return Ok(comments);
        }
    }
}
