using ConexiuniNonProfit.Data;
using ConexiuniNonProfit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Net.Mime.MediaTypeNames;

namespace ConexiuniNonProfit.Controllers
{
	public class PostsController : Controller
	{
		private readonly ApplicationDbContext db;

		private readonly UserManager<ApplicationUser> _userManager;

		private readonly RoleManager<IdentityRole> _roleManager;

		public PostsController(
			ApplicationDbContext context,
			UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager
			)
		{
			db = context;

			_userManager = userManager;

			_roleManager = roleManager;
		}
		[Authorize(Roles = "User,Admin")]
		public IActionResult Index()
		{
			ViewBag.existaprofil = 1;
			var posts = db.Posts.Include("User").Include("Group");

			//var groups = db.UserInGroups.Where(g => g.UserId == _userManager.GetUserId(User)).ToList();

			//ViewBag.existapostariingrup = 0;
			ViewBag.existapostariprieteni = 0;
			try
			{
				var userprof = db.Profiles.Where(p => p.UserId == _userManager.GetUserId(User)).First();

			}

			catch
			{
				ViewBag.existaprofil = 0;
			}

			// Se afiseaza formularul in care se vor completa datele unui articol
			// impreuna cu selectarea categoriei din care face parte
			// Doar utilizatorii cu rolul de Editor sau Admin pot adauga articole in platforma
			// HttpGet implicit

			var friends1 = db.Friends.Where(p => p.User1_Id == _userManager.GetUserId(User) && p.Accepted == true).ToList();
			var friends2 = db.Friends.Where(p => p.User2_Id == _userManager.GetUserId(User) && p.Accepted == true).ToList();

			List<string> frid = new List<string>();
			//List<Post> postari = new List<Post>();

			foreach (var fr in friends1)
			{
				frid.Add(fr.User2_Id);
			}

			foreach (var fr in friends2)
			{
				frid.Add(fr.User1_Id);
			}

			frid.Add(_userManager.GetUserId(User));
			/*
            foreach (var post in posts)
            {
                if (frid.Contains(post.UserId))
                    //postari.Add(post);

            }*/
			IEnumerable<Post> postari = db.Posts.Include("User").Where(p => frid.Contains(p.UserId));

			//List<Group> groups1 = new List<Group>();
			//foreach (var group in groups)
			//{
				//var grp = db.Groups.Where(g => g.GroupId == group.GroupId).First();
				//groups1.Add(grp);
			//}



			//IEnumerable<Post> postariingrup = db.Posts.Include("User").Include("Group").Where(p => groups1.Contains(p.Group) && p.GroupId != null);

			//if (postariingrup.Count() != 0)
			//{
			//	ViewBag.existapostariingrup = 1;
			//}

			if (postari.Count() != 0)
			{
				ViewBag.existapostariprieteni = 1;
			}
			// ViewBag.OriceDenumireSugestiva
			//postariingrup.Reverse();
			postari.Reverse();


			if (User.IsInRole("Admin"))
			{
				ViewBag.existapostariingrup = 1;
				ViewBag.existapostariprieteni = 1;
				ViewBag.Posts = posts;
				ViewBag.postariprieteni = posts;
			}
			else
			{
				//if (ViewBag.existapostariingrup == 1) ViewBag.Posts = postariingrup;
				if (ViewBag.existapostariprieteni == 1) ViewBag.postariprieteni = postari;
			}


			if (TempData.ContainsKey("message"))
			{
				ViewBag.Message = TempData["message"];
			}

			return View();
		}


		// Se afiseaza formularul in care se vor completa datele unui articol
		// impreuna cu selectarea categoriei din care face parte
		// Doar utilizatorii cu rolul de Editor sau Admin pot adauga articole in platforma
		// HttpGet implicit

		[Authorize(Roles = "User,Admin")]
		public IActionResult New()
		{
			Post post = new Post();
			ViewBag.Actiuni = new SelectList(db.Actiuni.ToList(), "ActiuniId", "ActiuniName");
			ViewBag.Categories = new SelectList(db.Categories.ToList(), "CategoryId", "CategoryName");
			return View();
		}

		[Authorize(Roles = "User,Admin")]
		[HttpPost]
		public IActionResult New(Post post, IFormFile? Image)
		{
			post.PostDate = DateTime.Now;
			post.UserId = _userManager.GetUserId(User);

			if (ModelState.IsValid)
			{
				if (Image != null && Image.Length > 0)
				{
					using (var memoryStream = new MemoryStream())
					{
						Image.CopyTo(memoryStream);
						post.Image = memoryStream.ToArray();
					}
				}

				db.Posts.Add(post);
				db.SaveChanges();
				TempData["message"] = "Postarea a fost adaugata";
				return RedirectToAction("Index");
			}
			else
			{
				ViewBag.Actiuni = new SelectList(db.Actiuni.ToList(), "ActiuniId", "ActiuniName");
				ViewBag.Categories = new SelectList(db.Categories.ToList(), "CategoryId", "CategoryName");
				return View(post);
			}
		}

		// Se editeaza un articol existent in baza de date impreuna cu categoria
		// din care face parte
		// Categoria se selecteaza dintr-un dropdown
		// HttpGet implicit
		// Se afiseaza formularul impreuna cu datele aferente articolului
		// din baza de date
		[Authorize(Roles = "Admin,User")]
		public IActionResult Edit(int id)
		{
			Post post = db.Posts
						   .Include(p => p.Actiuni)
						   .Include(p => p.Category)
						   .FirstOrDefault(p => p.PostId == id);

			if (post == null)
			{
				return NotFound();
			}

			ViewBag.Actiuni = new SelectList(db.Actiuni.ToList(), "ActiuniId", "ActiuniName", post.ActiuniId);
			ViewBag.Categories = new SelectList(db.Categories.ToList(), "CategoryId", "CategoryName", post.CategoryId);

			return View(post);
		}

		[HttpPost]
		[Authorize(Roles = "Admin,User")]
		public IActionResult Edit(int id, Post editedPost)
		{
			if (id != editedPost.PostId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					var post = db.Posts.Include(p => p.Actiuni).Include(p => p.Category).FirstOrDefault(p => p.PostId == id);

					if (post == null)
					{
						return NotFound();
					}

					post.PostContent = editedPost.PostContent;
					post.ActiuniId = editedPost.ActiuniId;
					post.CategoryId = editedPost.CategoryId;

					db.SaveChanges();

					TempData["message"] = "Postarea a fost modificata cu succes";
					return RedirectToAction("Index");
				}
				catch (Exception ex)
				{
					ModelState.AddModelError("", "A aparut o eroare la salvarea modificarii: " + ex.Message);
				}
			}

			ViewBag.Actiuni = new SelectList(db.Actiuni.ToList(), "ActiuniId", "ActiuniName", editedPost.ActiuniId);
			ViewBag.Categories = new SelectList(db.Categories.ToList(), "CategoryId", "CategoryName", editedPost.CategoryId);

			return View(editedPost);
		}

		// Se sterge un articol din baza de date 
		[HttpPost]
		[Authorize(Roles = "Admin,User")]
		public ActionResult Delete(int id)
		{
			Post post = db.Posts
										 .Include("Comments")
										 .Where(p => p.PostId == id)
										 .First();

			if (post.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
			{
				db.Posts.Remove(post);
				db.SaveChanges();
				TempData["message"] = "Postarea a fost stearsa";
				return RedirectToAction("Index");
			}

			else
			{
				TempData["message"] = "Nu aveti dreptul sa stergeti o postare care nu va apartine";
				return RedirectToAction("Index");
			}
		}

		[Authorize(Roles = "User,Admin")]
		public IActionResult Show(int id)
		{
			Post post = db.Posts
								 .Include(p => p.User)
								 .Include(p => p.Comments)
									 .ThenInclude(c => c.User)
								 .FirstOrDefault(p => p.PostId == id);

			if (post == null)
			{
				return NotFound();
			}

			ViewBag.profilid = db.Profiles.FirstOrDefault(p => p.UserId == post.UserId)?.ProfileId;
			SetAccessRights();

			// Setează numele categoriei și statului în ViewBag
			ViewBag.CategoryName = db.Categories.FirstOrDefault(c => c.CategoryId == post.CategoryId)?.CategoryName;
			ViewBag.ActiuniName = db.Actiuni.FirstOrDefault(s => s.ActiuniId == post.ActiuniId)?.ActiuniName;

			return View(post);
		}


		// Adaugarea unui comentariu asociat unui articol in baza de date
		[HttpPost]
		[Authorize(Roles = "User,Admin")]
		public IActionResult Show([FromForm] Comment comment)
		{
			comment.CommentDate = DateTime.Now;
			comment.UserId = _userManager.GetUserId(User);

			if (ModelState.IsValid)
			{
				db.Comments.Add(comment);
				db.SaveChanges();
				return Redirect("/Posts/Show/" + comment.PostId);
			}

			else
			{
				Post p = db.Posts
										 .Include("User")
										 .Include("Comments")
										 .Include("Comments.User")
										 .Where(p => p.PostId == comment.PostId)
										 .First();

				//return Redirect("/Articles/Show/" + comm.ArticleId);

				SetAccessRights();

				return View(p);
			}
		}

		[Authorize(Roles = "User,Admin")]
		public IActionResult SearchPosts(string search)
		{
			ViewBag.SearchString = search;

			if (string.IsNullOrEmpty(search))
			{
				TempData["message"] = "Introduceți un termen de căutare.";
				ViewBag.cautare = false;
				return View();
			}

			var posts = db.Posts
						  .Include(p => p.User)
						  .Where(p => p.PostContent.Contains(search))
						  .ToList();

			ViewBag.cautare = true;
			ViewBag.posts = posts;
			return View();
		}



		private void SetAccessRights()
		{
			ViewBag.AfisareButoane = false;

			ViewBag.EsteAdmin = User.IsInRole("Admin");

			ViewBag.UserCurent = _userManager.GetUserId(User);

			if (ViewBag.UserCurent == _userManager.GetUserId(User))
			{
				ViewBag.AfisareButoane = true;
			}
		}
	}
}