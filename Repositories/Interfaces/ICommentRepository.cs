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
        Task UpdateAsync(CommentEntity comment);
        Task DeleteAsync(CommentEntity commentEntity);
        Task<IEnumerable<CommentEntity>> GetByUserIdAsync(Guid userId);
    }
}
