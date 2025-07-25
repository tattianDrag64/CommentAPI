using CommentAPI.Data;
using CommentAPI.Entities;
using CommentAPI.Repositories.Interfaces;

namespace CommentAPI.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public async Task<CommentEntity> CreateAsync(CommentEntity comment)
        {
           return await _commentRepository.CreateAsync(comment);
        }

        public async Task<IEnumerable<CommentEntity>> GetAllAsync()
        {
            var tempData = await _commentRepository.GetAllAsync();
            return tempData;
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
            var tempData = await _commentRepository.GetByIdAsync(comment.ID);
            if (tempData == null)
            {
                return null ;
            }
                return tempData;
        }

        public async Task<bool> DeleteAsync(CommentEntity commentEntity)
        {
           var tempData = await _commentRepository.GetByIdAsync(commentEntity.ID);
            if (tempData != null)
            {
                await _commentRepository.DeleteAsync(tempData);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
