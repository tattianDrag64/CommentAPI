using CommentAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommentAPI.Repositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<CommentEntity>> GetAllAsync();
        Task<CommentEntity> GetByIdAsync(int id);
        Task<CommentEntity> CreateAsync(CommentEntity comment);
        Task<CommentEntity> UpdateAsync(CommentEntity comment);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<CommentEntity>> GetByUserIdAsync(Guid userId);
    }
}
