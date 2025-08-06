using CommentAPI.Data;
using CommentAPI.DTO;
using CommentAPI.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CommentAPI.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly CommentDBContext _context;
        public CommentRepository(CommentDBContext context)
        {
            _context = context;
        }
        public async Task<List<CommentDTO>> GetAllComments()
        {
            var tempData = await _context.Comments.ToListAsync();
            if (tempData == null || !tempData.Any())
            {
                throw new ArgumentNullException(nameof(tempData), "No Comments Found!");
            }
            return tempData.Select(comment => new CommentDTO
            {
                ID = comment.ID,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                UpdatedAt = comment.UpdatedAt,
                UserID = comment.UserID,
                EventID = comment.EventID
            }).ToList();
        }
        public async Task<CommentDTO> GetByIdComment(int id)
        {
            var tempData = await _context.Comments.FindAsync(id);
            if (tempData == null)
            {
                throw new ArgumentNullException(nameof(tempData), "No Comment Found!");
            }
            return new CommentDTO
            {
                ID = tempData.ID,
                Content = tempData.Content,
                CreatedAt = tempData.CreatedAt,
                UpdatedAt = tempData.UpdatedAt,
                UserID = tempData.UserID,
                EventID = tempData.EventID
            };
        }
        public async Task<CreateCommentDto> CreateComment(CreateCommentDto comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment), "Comment can't be null!");
            }

            var commentEntity = new CommentEntity
            {
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                UserID = comment.UserID,
                EventID = comment.EventID
            };

            _context.Comments.Add(commentEntity);
            await _context.SaveChangesAsync();

            comment.ID = commentEntity.ID;
            return comment;
        }
        public async Task<UpdateCommentDto> UpdateComment(UpdateCommentDto comment)
        {
            var tempData = await _context.Comments.FindAsync(comment.ID);
            if (tempData == null)
            {
                throw new ArgumentNullException(nameof(comment), "No Comment Found!");
            }
            tempData.Content = comment.Content;
            tempData.UpdatedAt = DateTime.Now;
            _context.Comments.Update(tempData);
            await _context.SaveChangesAsync();
            return new UpdateCommentDto
            {
                ID = tempData.ID,
                Content = tempData.Content,
                UpdatedAt = tempData.UpdatedAt
            };
        }
        public async Task<bool> DeleteComment(int id)
        {
            var tempData = await _context.Comments.FindAsync(id);
            if (tempData == null)
            {
                return false;
            }
            _context.Comments.Remove(tempData);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<CommentDTO>> GetByUserIdComment(Guid userId)
        {
            var tempData = await _context.Comments.Where(c => c.UserID == userId).ToListAsync();
            if (tempData == null || !tempData.Any())
            {
                throw new ArgumentNullException(nameof(tempData), "No User Found!");
            }
            return tempData.Select(comment => new CommentDTO
            {
                ID = comment.ID,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                UpdatedAt = comment.UpdatedAt,
                UserID = comment.UserID,
                EventID = comment.EventID
            }).ToList();
        }

        public async Task<List<CommentDTO>> GetByEventIdComment(int eventId)
        {
            var tempData = await _context.Comments.Where(c => c.EventID == eventId).ToListAsync();
            if (tempData == null || !tempData.Any())
            {
                throw new ArgumentNullException(nameof(tempData), "No Event Found!");
            }
            return tempData.Select(comment => new CommentDTO
            {
                ID = comment.ID,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                UpdatedAt = comment.UpdatedAt,
                UserID = comment.UserID,
                EventID = comment.EventID
            }).ToList();
        }
    }
}
