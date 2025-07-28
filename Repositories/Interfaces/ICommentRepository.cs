using CommentAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommentAPI.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<CommentEntity>> GetAllAsync();
        Task<CommentEntity> GetByIdAsync(Guid id);
        Task<CommentEntity> CreateAsync(CommentEntity comment);
        Task<CommentEntity> UpdateAsync(CommentEntity comment);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<CommentEntity>> GetByUserIdAsync(Guid userId);
    }
}
