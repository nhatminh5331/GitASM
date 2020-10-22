using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuyenMinhFinal.Models;

namespace TuyenMinhFinal.Controllers
{
	public class CategoriesController : Controller
	{

		private ApplicationDbContext _context;
		public CategoriesController()
		{
			_context = new ApplicationDbContext();
		}
		// GET: Course
		[HttpGet]
		public ActionResult Index()
		{
			var categories = _context.Categories.ToList();
			return View(categories);

		}

		[HttpGet]
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(Category category)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			if (_context.Categories.Any(p => p.Name.Contains(category.Name)))
			{
				ModelState.AddModelError("Name", "Category Name Already Exists.");
				return View();
			}

			var newCategory = new Category
			{
				Name = category.Name,
				Description = category.Description,


			};

			_context.Categories.Add(newCategory);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			var categoryInDb = _context.Categories.SingleOrDefault(p => p.Id == id);

			if (categoryInDb == null)
			{
				return HttpNotFound();
			}

			_context.Categories.Remove(categoryInDb);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			var categoryInDb = _context.Categories.SingleOrDefault(p => p.Id == id);

			if (categoryInDb == null)
			{
				return HttpNotFound();
			}

			return View(categoryInDb);
		}

		[HttpPost]
		public ActionResult Edit(Category category)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			var categoryInDb = _context.Categories.SingleOrDefault(p => p.Id == category.Id);

			if (categoryInDb == null)
			{
				return HttpNotFound();
			}

			categoryInDb.Name = category.Name;
			categoryInDb.Description = category.Description;

			_context.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}