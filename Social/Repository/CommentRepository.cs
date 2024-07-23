using Microsoft.EntityFrameworkCore;
using Social.Data;
using Social.Interfaces;
using Social.Models;

namespace Social.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;

        public CommentRepository(ApplicationDBContext context)
        {
            this._context = context;
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _context.comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var comment = await _context.comments.FirstOrDefaultAsync(x => x.Id == id);
            if (comment == null)
            {
                return null;
            }
            _context.comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;



        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.comments.Include(a=>a.AppUser).ToListAsync();

        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var commentModel = await _context.comments.Include(a => a.AppUser).FirstOrDefaultAsync(a=>a.Id==id);
            if (commentModel == null)
            {
                return null;
            }
            return commentModel;
        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
            var exictingcomment = await _context.comments.FindAsync(id);
            if (exictingcomment == null)
            {
                return null;
            }
            exictingcomment.Title = commentModel.Title;
            exictingcomment.Content = commentModel.Content;
            await _context.SaveChangesAsync();
            return exictingcomment;
        }
    }
}
