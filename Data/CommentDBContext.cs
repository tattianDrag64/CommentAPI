using CommentAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommentAPI.Data
{
    public class CommentDBContext : DbContext
    {
        public CommentDBContext(DbContextOptions<CommentDBContext> options) : base(options)
        {
        }
        public DbSet<CommentEntity> Comments { get; set; }
        public DbSet<UserModel> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
    }