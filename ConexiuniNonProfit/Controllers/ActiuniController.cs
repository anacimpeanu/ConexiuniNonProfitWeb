using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ConexiuniNonProfit.Models;
using ConexiuniNonProfit.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ConexiuniNonProfit.Data;
using ConexiuniNonProfit.Models;

namespace ConexiuniNonProfit.Controllers
{
	public class ActiuniController : Controller
	{
		private readonly ApplicationDbContext _db;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public ActiuniController(ApplicationDbContext context,
			UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager)
		{
			_db = context;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		[Authorize(Roles = "Admin, User")]
		public ActionResult Index()
		{
			if (TempData.ContainsKey("Message"))
			{
				ViewBag.Message = TempData["Message"].ToString();
			}

			var Actiuni = _db.Actiuni.OrderBy(Actiuni => Actiuni.ActiuniName).ToList();
			return View(Actiuni);
		}

		[Authorize(Roles = "Admin, User")]
		public IActionResult PostsByActiuni(int ActiuniId)
		{
			var posts = _db.Posts.Include(p => p.Actiuni)
								 .Include(p => p.User)
								 .Include(p => p.Comments)
								 .Where(p => p.ActiuniId == ActiuniId)
								 .OrderByDescending(b => b.PostDate)
								 .ToList();

			ViewBag.Posts = posts;
			ViewBag.SelectedActiuniId = ActiuniId;

			return View("Index", posts);
		}

		[Authorize(Roles = "Admin")]
		public ActionResult New()
		{
			return View();
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public ActionResult New(Actiuni Actiuni)
		{
			if (ModelState.IsValid)
			{
				_db.Actiuni.Add(Actiuni);
				_db.SaveChanges();
				TempData["Message"] = "Actiuni added successfully!";
				return RedirectToAction("Index");
			}
			else
			{
				return View(Actiuni);
			}
		}

		[Authorize(Roles = "User, Admin")]
		public IActionResult Show(int id)
		{
			var Actiuni = _db.Actiuni
						   .Include(s => s.Posts)
						   .ThenInclude(p => p.User) // Ensure User is included
						   .FirstOrDefault(s => s.ActiuniId == id);

			if (Actiuni == null)
			{
				TempData["Message"] = "Actiuni was not found";
				TempData["MessageType"] = "alert-danger";
				return RedirectToAction("Index");
			}

			return View(Actiuni);
		}

		[Authorize(Roles = "Admin")]
		public ActionResult Edit(int id)
		{
			var Actiuni = _db.Actiuni.Find(id);
			return View(Actiuni);
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public ActionResult Edit(int id, Actiuni requestActiuni)
		{
			var Actiuni = _db.Actiuni.Find(id);

			if (Actiuni == null)
			{
				TempData["Message"] = "Actiuni not found";
				TempData["MessageType"] = "alert-danger";
				return RedirectToAction("Index");
			}

			if (ModelState.IsValid)
			{
				Actiuni.ActiuniName = requestActiuni.ActiuniName;
				Actiuni.ActiuniAbbreviation = requestActiuni.ActiuniAbbreviation;
				Actiuni.ActiuniDescription = requestActiuni.ActiuniDescription;
				_db.SaveChanges();
				TempData["Message"] = "Actiuni modified";
				return RedirectToAction("Index");
			}
			else
			{
				return View(requestActiuni);
			}
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public ActionResult Delete(int id)
		{
			var Actiuni = _db.Actiuni.Find(id);
			if (Actiuni != null)
			{
				var postsToDelete = _db.Posts.Where(p => p.ActiuniId == id);
				foreach (var post in postsToDelete)
				{
					var commentsToDelete = _db.Comments.Where(c => c.PostId == post.PostId);
					_db.Comments.RemoveRange(commentsToDelete);
					_db.Posts.Remove(post);
				}

				_db.Actiuni.Remove(Actiuni);
				_db.SaveChanges();

				TempData["Message"] = "Actiuni deleted";
			}
			else
			{
				TempData["Message"] = "Actiuni not found";
				TempData["MessageType"] = "alert-danger";
			}

			return RedirectToAction("Index");
		}
	}
}
