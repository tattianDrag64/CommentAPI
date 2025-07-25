using CommentAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommentAPI.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<CommentEntity>> GetAllAsync();
        Task<CommentEntity> GetByIdAsync(int id);
        Task<CommentEntity> CreateAsync(CommentEntity comment);
        Task UpdateAsync(CommentEntity comment);
        Task DeleteAsync(CommentEntity commentEntity);
        Task<IEnumerable<CommentEntity>> GetByUserIdAsync(int userId);
    }
}
