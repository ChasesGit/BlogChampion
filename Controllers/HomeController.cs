using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogChampion.Models;
using BlogChampion.Data;
using BlogChampion.Data.Repository;

namespace BlogChampion.Controllers
{
	public class HomeController : Controller
	{
        private IRepository _repo;

        public HomeController(IRepository repo)
		{
			_repo = repo;
		}
		public IActionResult Index()
		{
			var posts = _repo.GetAllPosts();
			return View(posts);
		}

		public IActionResult Post(int id)
		{
			var post = _repo.getPost(id); 
			return View(post);
		}

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
        public async Task<IActionResult> Remove(int id)
        {
			_repo.removePost(id);
			await _repo.SaveChangesAsync();
			return RedirectToAction("Index");

           
        }
    }
}
