using CommentAPI.Data;
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
        public async Task<IEnumerable<CommentEntity>> GetAllAsync()
        {
            var tempData = await _context.Comments.ToListAsync();
            if (tempData == null || !tempData.Any())
            {
                throw new ArgumentNullException(nameof(tempData));
            }
            return tempData;
        }
        public async Task<CommentEntity> GetByIdAsync(int id)
        {
            var tempData = await _context.Comments.FindAsync(id);
            if (tempData == null)
            {
                throw new ArgumentNullException(nameof(tempData));
            }
            return tempData;
        }
        public async Task<CommentEntity> CreateAsync(CommentEntity comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
        public async Task<CommentEntity> UpdateAsync(CommentEntity comment)
        {
            var tempData = await _context.Comments.FindAsync(comment.ID);
            if (tempData == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }
            _context.Comments.Update(tempData);
            await _context.SaveChangesAsync();
            return tempData;
        }
        public async Task<bool> DeleteAsync(int id)
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
        public async Task<IEnumerable<CommentEntity>> GetByUserIdAsync(Guid userId)
        {
            var tempData = await _context.Comments.Where(c => c.UserID == userId).ToListAsync();
            if (tempData == null || !tempData.Any())
            {
                throw new ArgumentNullException(nameof(tempData));
            }
            return tempData;
        }


    }
}
