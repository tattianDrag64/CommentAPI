using CommentAPI.Entities;

namespace CommentAPI.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentEntity>> GetAllAsync();
        Task<CommentEntity> GetByIdAsync(int id);
        Task<CommentEntity> CreateAsync(CommentEntity comment);
        Task<CommentEntity> UpdateAsync(CommentEntity comment);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<CommentEntity>> GetByUserIdAsync(Guid userId);
    }
}
