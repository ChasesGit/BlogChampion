using BlogChampion.Models;

namespace BlogChampion.Data.Repository
{
    public interface IRepository
    {

        Post getPost(int id);
        List<Post> GetAllPosts();
        void addPost(Post post);
        void removePost(int id);

        void UpdatePost(Post post);

        Task<bool> SaveChangesAsync();
    }
}
