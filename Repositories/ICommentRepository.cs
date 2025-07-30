using CommentAPI.DTO;
using CommentAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommentAPI.Repositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<CommentDTO>> GetAllAsync();
        Task<CommentDTO> GetByIdAsync(int id);
        Task<CreateCommentDto> CreateAsync(CreateCommentDto comment);
        Task<UpdateCommentDto> UpdateAsync(UpdateCommentDto comment);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<CommentDTO>> GetByUserIdAsync(Guid userId);
    }
}
