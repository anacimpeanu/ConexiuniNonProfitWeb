using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ConexiuniNonProfit.Models;
using ConexiuniNonProfit.Data;
using Microsoft.EntityFrameworkCore;
using ConexiuniNonProfit.Data;
using ConexiuniNonProfit.Models;

namespace ConexiuniNonProfit.Controllers
{
	public class CategoriesController : Controller
	{
		private readonly ApplicationDbContext _db;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public CategoriesController(ApplicationDbContext context,
			UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager)
		{
			_db = context;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		[Authorize(Roles = "Admin, User")]
		[AllowAnonymous]
		public ActionResult Index()
		{
			if (TempData.ContainsKey("Message"))
			{
				ViewBag.Message = TempData["Message"].ToString();
			}

			var categories = from category in _db.Categories
							 orderby category.CategoryName
							 select category;

			ViewBag.Categories = categories;

			return View();
		}

		[Authorize(Roles = "Admin,User")]
		[AllowAnonymous]
		public IActionResult PostsByCategory(int categoryId)
		{
			var posts = _db.Posts.Include("Category")
									.Include("User")
									.Include("Comments")
									.Where(p => p.CategoryId == categoryId)
									.OrderByDescending(b => b.PostDate)
									.ToList();

			ViewBag.Posts = posts;
			ViewBag.SelectedCategoryId = categoryId;

			return View("Index");
		}

		[Authorize(Roles = "Admin")]
		public ActionResult New()
		{
			return View();
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public ActionResult New(Category category)
		{
			if (ModelState.IsValid)
			{
				_db.Categories.Add(category);
				_db.SaveChanges();
				TempData["Message"] = "Category added successfully!";
				return RedirectToAction("Index");
			}
			else
			{
				return View(category);
			}
		}

		[Authorize(Roles = "User,Admin")]
		[AllowAnonymous]
		public IActionResult Show(int id)
		{
			var category = _db.Categories
				.Include(c => c.Posts)
				// .ThenInclude(p => p.Comments)
				.ThenInclude(p => p.User) // Ensure User is included
				.FirstOrDefault(c => c.CategoryId == id);

			if (category == null)
			{
				TempData["Message"] = "Category was not found";
				TempData["MessageType"] = "alert-danger";
				return RedirectToAction("Index");
			}

			return View(category);
		}

		[Authorize(Roles = "Admin")]
		public ActionResult Edit(int id)
		{
			var category = _db.Categories.Find(id);
			return View(category);
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public ActionResult Edit(int id, Category requestCategory)
		{
			var category = _db.Categories.Find(id);

			if (ModelState.IsValid)
			{
				category.CategoryName = requestCategory.CategoryName;
				_db.SaveChanges();
				TempData["Message"] = "Category modified";
				return RedirectToAction("Index");
			}
			else
			{
				return View(requestCategory);
			}
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public ActionResult Delete(int id)
		{
			var category = _db.Categories.Find(id);
			var postsToDelete = _db.Posts.Where(p => p.CategoryId == id);
			foreach (var post in postsToDelete)
			{
				var commentsToDelete = _db.Comments.Where(c => c.PostId == post.PostId);
				_db.Comments.RemoveRange(commentsToDelete);
				_db.Posts.Remove(post);
			}

			_db.Categories.Remove(category);

			TempData["Message"] = "Category deleted";
			_db.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}
