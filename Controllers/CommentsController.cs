using CommentAPI.DTO;
using CommentAPI.Entities;
using CommentAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CommentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : Controller
    {
        private readonly ICommentService _service;

        public CommentsController(ICommentService CommentService)
        {
            _service = CommentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetAllAsync()
        {
            var comments = await _service.GetAllAsync();
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDTO>> GetByIdAsync(int id)
        {
            var comment = await _service.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        [HttpPost]
        [Authorize(Roles = "User, Admin")]
        public async Task<ActionResult<CommentDTO>> CreateAsync([FromBody] CreateCommentDto createDto)
        {
            if (createDto == null)
            {
                return BadRequest("Comment cannot be null");
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out Guid userGuid))
            {
                return Unauthorized("Invalid user ID");
            }

            var createdComment = await _service.CreateAsync(createDto);
            return Ok(createdComment);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateCommentDto updateDto)
        {
            if (updateDto == null)
            {
                return BadRequest("Comment cannot be null");
            }

            var existingComment = await _service.GetByIdAsync(id);
            if (existingComment == null)
            {
                return NotFound();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out Guid userGuid))
            {
                return Unauthorized("Invalid user ID");
            }

            if (existingComment.UserID != userGuid)
            {
                return Forbid();
            }
            var UpdatedTemp = await _service.UpdateAsync(updateDto);
            return Ok(UpdatedTemp);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var existingComment = await _service.GetByIdAsync(id);
            if (existingComment == null)
            {
                return NotFound();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out Guid userGuid))
            {
                return Unauthorized("Invalid user ID");
            }

            if (existingComment.UserID != userGuid && userRole != "Admin")
            {
                return Forbid();
            }

            await _service.DeleteAsync(id);
            return Ok();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetByUserIdAsync(Guid userId)
        {
            var comments = await _service.GetByUserIdAsync(userId);
            if (comments == null || !comments.Any())
            {
                return NotFound();
            }
            var commentDtos = comments.Select(c => new CommentDTO
            {
                ID = c.ID,
                Content = c.Content,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                UserID = c.UserID,
                EventID = c.EventID
            });
            return Ok(commentDtos);
        }
    }
}
