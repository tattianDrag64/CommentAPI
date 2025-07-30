using CommentAPI.DTO;
using CommentAPI.Entities;

namespace CommentAPI.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDTO>> GetAllAsync();
        Task<CommentDTO> GetByIdAsync(int id);
        Task<CreateCommentDto> CreateAsync(CreateCommentDto comment);
        Task<UpdateCommentDto> UpdateAsync(CommentDTO comment);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<CommentDTO>> GetByUserIdAsync(Guid userId);
    }
}
