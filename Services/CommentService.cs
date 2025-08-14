using CommentAPI.Data;
using CommentAPI.DTO;
using CommentAPI.Entities;
using CommentAPI.Repositories;

namespace CommentAPI.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<CreateCommentDto> CreateComment(CreateCommentDto comment)
        {
            var commentDto = new CreateCommentDto
            {
                ID = comment.ID,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                UserID = comment.UserID,
                EventID = comment.EventID
            };
            return await _commentRepository.CreateComment(commentDto);
        }

        public async Task<IEnumerable<CommentDTO>> GetAllComments()
        {
            return await _commentRepository.GetAllComments();
        }

        public async Task<CommentDTO> GetByIdComment(int id)
        {
            return await _commentRepository.GetByIdComment(id);
        }

        public async Task<List<CommentDTO>> GetByUserIdComment(Guid userId)
        {
            return await _commentRepository.GetByUserIdComment(userId);
        }

        public async Task<UpdateCommentDto> UpdateComment(CommentDTO comment)
        {
            var existingComment = await _commentRepository.GetByIdComment(comment.ID);
            if (existingComment == null)
            {
                throw new ArgumentNullException(nameof(existingComment), "No Comment Found!");
            }
            var updateCommentDto = new UpdateCommentDto
            {
                ID = existingComment.ID,
                Content = comment.Content,
                UpdatedAt = DateTime.UtcNow
            };
            await _commentRepository.UpdateComment(updateCommentDto);
            return updateCommentDto;
        }

        public async Task<bool> DeleteComment(int id)
        {
            return await _commentRepository.DeleteComment(id);
        }

        public async Task<List<CommentDTO>> GetByEventIdComment(int eventId)
        {
            return await _commentRepository.GetByEventIdComment(eventId);
        }

        public async Task<List<CommentDTO>> GetParentComments(int replyID)
        {
            return await _commentRepository.GetParentComment(replyID);
        }

        public async Task<List<CommentDTO>> GetCommentReplies(int commentId)
        {
            return await _commentRepository.GetReplies(commentId);
        }

        public async Task<List<CommentDTO>> GetTopLevelComments(int eventId)
        {
            return await _commentRepository.GetTopLevelComments(eventId);
        }
    }
}
