using BlogChampion.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BlogChampion.Data.Repository
{
    public class Repository : IRepository
    {
        private AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }
        public void addPost(Post post)
        {
            _context.Posts.Add(post);
            
        }

        public List<Post> GetAllPosts()
        {
            return _context.Posts.ToList();
        }

        public Post getPost(int id)
        {
            return _context.Posts.FirstOrDefault(p => p.Id == id);
        }

        public void removePost(int id)
        {
            _context.Posts.Remove(getPost(id));
        }

        public void UpdatePost(Post post)
        {
            _context.Posts.Update(post);
        }

        public async Task<bool> SaveChangesAsync()
        {
            if(await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
