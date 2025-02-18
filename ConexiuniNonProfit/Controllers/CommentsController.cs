﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ConexiuniNonProfit.Data;
using ConexiuniNonProfit.Models;
using System.Data;
using ConexiuniNonProfit.Data;
using ConexiuniNonProfit.Models;

namespace ConexiuniNonProfit.Controllers
{
	public class CommentsController : Controller
	{
		private readonly ApplicationDbContext db;

		private readonly UserManager<ApplicationUser> _userManager;

		private readonly RoleManager<IdentityRole> _roleManager;

		public CommentsController(
			ApplicationDbContext context,
			UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager
			)
		{
			db = context;

			_userManager = userManager;

			_roleManager = roleManager;
		}


		// Stergerea unui comentariu asociat unui articol din baza de date
		[HttpPost]
		[Authorize(Roles = "User,Admin")]
		public IActionResult Delete(int id)
		{
			Comment comm = db.Comments.Find(id);

			if (comm.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
			{
				db.Comments.Remove(comm);
				db.SaveChanges();
				return Redirect("/Posts/Show/" + comm.PostId);
			}

			else
			{
				TempData["message"] = "Nu aveti dreptul sa stergeti comentariul";
				return RedirectToAction("Index", "Posts");
			}
		}

		// In acest moment vom implementa editarea intr-o pagina View separata
		// Se editeaza un comentariu existent
		[Authorize(Roles = "User,Admin")]
		public IActionResult Edit(int id)
		{
			Comment comm = db.Comments.Find(id);

			if (comm.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
			{
				return View(comm);
			}

			else
			{
				TempData["message"] = "Nu aveti dreptul sa editati comentariul";
				return RedirectToAction("Index", "Posts");
			}
		}

		[HttpPost]
		[Authorize(Roles = "User,Admin")]
		public IActionResult Edit(int id, Comment requestComment)
		{
			Comment comm = db.Comments.Find(id);


			if (comm.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
			{
				if (ModelState.IsValid)
				{
					comm.CommentContent = requestComment.CommentContent;

					db.SaveChanges();

					return Redirect("/Posts/Show/" + comm.PostId);
				}
				else
				{
					return View(requestComment);
				}
			}
			else
			{
				TempData["message"] = "Nu aveti dreptul sa faceti modificari";
				return RedirectToAction("Index", "Posts");
			}



		}
	}
}
