using CommentAPI.DTO;
using CommentAPI.Entities;

namespace CommentAPI.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDTO>> GetAllComments();
        Task<CommentDTO> GetByIdComment(int id);
        Task<CreateCommentDto> CreateComment(CreateCommentDto comment);
        Task<UpdateCommentDto> UpdateComment(CommentDTO comment);
        Task<bool> DeleteComment(int id);
        Task<List<CommentDTO>> GetByUserIdComment(Guid userId);
        Task<List<CommentDTO>> GetByEventIdComment(int eventId);
    }
}
