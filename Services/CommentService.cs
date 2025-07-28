using CommentAPI.Data;
using CommentAPI.Entities;
using CommentAPI.Repositories.Interfaces;

namespace CommentAPI.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<CommentEntity> CreateAsync(CommentEntity comment)
        {
            var commentTemp = new CommentEntity()
            {
                Content = comment.Content,
                EventID = comment.EventID,
                UserID = comment.UserID,
                CreatedAt = comment.CreatedAt,
                ID = comment.ID
            };
           return await _commentRepository.CreateAsync(commentTemp);
        }

        public async Task<IEnumerable<CommentEntity>> GetAllAsync()
        {
            return await _commentRepository.GetAllAsync();
        }

        public async Task<CommentEntity> GetByIdAsync(Guid id)
        {
            return await _commentRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<CommentEntity>> GetByUserIdAsync(Guid userId)
        {
            return await _commentRepository.GetByUserIdAsync(userId);
        }

        public async Task<CommentEntity> UpdateAsync(CommentEntity comment)
        {
            return await _commentRepository.UpdateAsync(comment);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
        return await _commentRepository.DeleteAsync(id);
        }
    }
}
