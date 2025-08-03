using CommentAPI.DTO;
using CommentAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommentAPI.Repositories
{
    public interface ICommentRepository
    {
        Task<List<CommentDTO>> GetAllComments();
        Task<CommentDTO> GetByIdComment(int id);
        Task<CreateCommentDto> CreateComment(CreateCommentDto comment);
        Task<UpdateCommentDto> UpdateComment(UpdateCommentDto comment);
        Task<bool> DeleteComment(int id);
        Task<List<CommentDTO>> GetByUserIdComment(Guid userId);
        Task<List<CommentDTO>> GetByEventIdComment(int eventId);
    }
}
