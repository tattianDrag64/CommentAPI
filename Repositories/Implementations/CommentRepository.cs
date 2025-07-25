using CommentAPI.Data;
using CommentAPI.Entities;
using CommentAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommentAPI.Repositories.Implementations
{
    public class CommentRepository : ICommentRepository
    {
        private readonly CommentDBContext _context;
        public CommentRepository(CommentDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CommentEntity>> GetAllAsync()
        {
            var tempData = await _context.Comments.ToListAsync();
            if (tempData == null || !tempData.Any())
            {
                return null;
            }
            return tempData;
        }
        public async Task<CommentEntity> GetByIdAsync(Guid id)
        {
            return await _context.Comments.FindAsync(id);
        }
        public async Task<CommentEntity> CreateAsync(CommentEntity comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
        public async Task<CommentEntity> UpdateAsync(CommentEntity comment)
        {
            var tempData = await _context.Comments.FindAsync(comment.ID);
            if (tempData == null)
            {
                return null;
            }
            _context.Comments.Update(tempData);
            await _context.SaveChangesAsync();
            return tempData;
        }
        public async Task<bool> DeleteAsync(CommentEntity commentEntity)
        {
            var tempData = await _context.Comments.FindAsync(commentEntity.ID);
            if(tempData == null)
            {
                return false;
            }
            _context.Comments.Remove(tempData);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<CommentEntity>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Comments.Where(c => c.UserID == userId).ToListAsync();
        }

       
    }
}
