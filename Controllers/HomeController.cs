using Microsoft.AspNetCore.Mvc;
using BlogChampion.Models;
using BlogChampion.Data.Repository;

namespace BlogChampion.Controllers
{
	public class HomeController : Controller
	{
		//Private variable to be used for connecting the repository databases of the post into the public interface.
        private IRepository _repo;

        //Public Constructor that instatiates the database we are pulling the information from.
        public HomeController(IRepository repo)
		{
			_repo = repo;
		}

        //Public Function that responds to an action (such as clicking) that then grabs all the posts currently in the database and returns a view of the list of posts,
        // which we can iterate through to get individuals posts.
        public IActionResult Index()
		{
			var posts = _repo.GetAllPosts();
			return View(posts);
		}

        //Public Function that responds to an action (such as clicking) that then will return the individual post clicked on by passing a id that is stored into our post object.
        public IActionResult Post(int id)
		{
			var post = _repo.getPost(id); 
			return View(post);
		}

        //Public Function that responds to an action (such as clicking) and has a nullable id that is passed to it, if no id is passed to it a new post is created else it finds the post specified by the ID passed into it.
		//Get whatever post is clicked or when submit is hit.
        [HttpGet]
		public IActionResult Change(int? id) 
		{ 
			if(id == null)
			{
                return View(new Post());
            }
			else
			{
				var post = _repo.getPost((int) id);
				return View(post);
			}
		}
		//This is the method that will actually make the changes to the website. It looks for a task such as click and depending on if the Id of the post that is passed into it will either look to create a post (such as when it has no id when you create the post)
		//Or it will update the post
		[HttpPost]
        public async Task<IActionResult> ChangeAsync(Post post)
        {
			if(post.Id > 0)
			{
				_repo.UpdatePost(post); 
			}
			else
			{
                _repo.addPost(post);
            }
			
			if (await _repo.SaveChangesAsync())
			{
				return RedirectToAction("Index");
			}
			else 
				return View(); 
        }
        [HttpGet]
        //This is method will remove whatever post id is passed into it by calling the removePost function inside of repository object.
        public async Task<IActionResult> Remove(int id)
        {
			_repo.removePost(id);
			await _repo.SaveChangesAsync();
			return RedirectToAction("Index");

           
        }
    }
}
