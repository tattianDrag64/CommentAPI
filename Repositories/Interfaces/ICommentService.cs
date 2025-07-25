using CommentAPI.Entities;

namespace CommentAPI.Repositories.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentEntity>> GetAllAsync();
        Task<CommentEntity> GetByIdAsync(Guid id);
        Task<CommentEntity> CreateAsync(CommentEntity comment);
        Task<CommentEntity> UpdateAsync(CommentEntity comment);
        Task<bool> DeleteAsync(CommentEntity commentEntity);
        Task<IEnumerable<CommentEntity>> GetByUserIdAsync(Guid userId);
    }
}
