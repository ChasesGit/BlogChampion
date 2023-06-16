using BlogChampion.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BlogChampion.Data.Repository
{
    //Used for connecting to our homecontroller and controlling our posts such as removing getting and upating. This is also where we made our boolean for how long our Changes take to update the website.
    //We use async to free up the thread for other activies.
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
