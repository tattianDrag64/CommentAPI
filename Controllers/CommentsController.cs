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
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetAllComments()
        {
            var comments = await _service.GetAllComments();
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDTO>> GetByIdComment(int id)
        {
            var comment = await _service.GetByIdComment(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        [HttpPost]
        [Authorize(Roles = "User, Admin")]
        public async Task<ActionResult<CommentDTO>> CreateComment([FromBody] CreateCommentDto createDto)
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

            var createdComment = await _service.CreateComment(createDto);
            return Ok(createdComment);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] UpdateCommentDto updateDto)
        {
            if (updateDto == null)
            {
                return BadRequest("Comment cannot be null");
            }

            var existingComment = await _service.GetByIdComment(id);
            if (existingComment == null)
            {
                return NotFound();
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out Guid userGuid))
            {
                return Unauthorized("Invalid user ID");
            }

            if (existingComment.UserID != userGuid)
            {
                return Forbid();
            }

            var commentToUpdate = new CommentDTO
            {
                ID = id,
                Content = updateDto.Content,
                UpdatedAt = updateDto.UpdatedAt,
                UserID = existingComment.UserID,
                EventID = existingComment.EventID
            };

            var updatedComment = await _service.UpdateComment(commentToUpdate);
            return Ok(updatedComment);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var existingComment = await _service.GetByIdComment(id);
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

            await _service.DeleteComment(id);
            return Ok();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetByUserIdComment(Guid userId)
        {
            var comments = await _service.GetByUserIdComment(userId);
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

        [HttpGet("event/{eventId}")]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetByEventIdComment(int eventId)
        {
            var comments = await _service.GetByEventIdComment(eventId);
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
