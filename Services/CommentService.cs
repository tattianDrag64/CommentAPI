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

        public async Task<CreateCommentDto> CreateAsync(CreateCommentDto comment)
        {
            var commentDto = new CreateCommentDto
            {
                ID = comment.ID,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                UserID = comment.UserID,
                EventID = comment.EventID
            };
            return await _commentRepository.CreateAsync(commentDto);
        } 

        public async Task<IEnumerable<CommentDTO>> GetAllAsync()
        {
            return await _commentRepository.GetAllAsync();
        }

        public async Task<CommentDTO> GetByIdAsync(int id)
        {
            return await _commentRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<CommentDTO>> GetByUserIdAsync(Guid userId)
        {
            return await _commentRepository.GetByUserIdAsync(userId);
        }

        public async Task<UpdateCommentDto> UpdateAsync(CommentDTO comment)
        {
            var existingComment = await _commentRepository.GetByIdAsync(comment.ID);
            if (existingComment == null)
            {
                return null; // Or throw an exception if preferred
            }
           var updateCommentDto = new UpdateCommentDto
            {
                ID = existingComment.ID,
                Content = comment.Content,
                UpdatedAt = DateTime.UtcNow
            };
            await _commentRepository.UpdateAsync(updateCommentDto);
            return updateCommentDto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _commentRepository.DeleteAsync(id);
        }
    }
}
