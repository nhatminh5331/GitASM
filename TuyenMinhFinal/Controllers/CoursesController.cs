using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuyenMinhFinal.Models;
using TuyenMinhFinal.ViewModels;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TuyenMinhFinal.Controllers
{
    public class CoursesController : Controller
    {
        
        

		private ApplicationDbContext _context;
		public CoursesController()
		{
			_context = new ApplicationDbContext();
		}
		// GET: Staff
		[HttpGet]
		public ActionResult Index(string searchString)
		{
			var courses = _context.Courses
			.Include(p => p.Category).Include(c => c.Topic);

			if (!String.IsNullOrEmpty(searchString))
			{
				courses = courses.Where(
					s => s.Name.Contains(searchString) ||
					s.Category.Name.Contains(searchString) ||
					s.Topic.Name.Contains(searchString));

			}

			return View(courses.ToList());
		}

		[HttpGet]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Create()
		{
			var viewModel = new CourseCategoryViewModel
			{
				Categories = _context.Categories.ToList(),
				Topics = _context.Topics.ToList()
			};
			return View(viewModel);
		}

		[HttpPost]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Create(Course course)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			var newCourse = new Course
			{
				Name = course.Name,
				CategoryId = course.CategoryId,
				TopicId = course.TopicId,
			};

			_context.Courses.Add(newCourse);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpGet]
		[Authorize(Roles = "TrainingStaff")]

		public ActionResult Delete(int id)
		{
			var courseInDb = _context.Courses.SingleOrDefault(p => p.Id == id);

			if (courseInDb == null)
			{
				return HttpNotFound();
			}

			_context.Courses.Remove(courseInDb);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpGet]
		[Authorize(Roles = "TrainingStaff")]

		public ActionResult Edit(int id)
		{
			var courseInDb = _context.Courses.SingleOrDefault(p => p.Id == id);

			if (courseInDb == null)
			{
				return HttpNotFound();
			}

			var viewModel = new CourseCategoryViewModel
			{
				Course = courseInDb,
				Categories = _context.Categories.ToList(),
				Topics = _context.Topics.ToList()
			};

			return View(viewModel);
		}

		[HttpPost]
		[Authorize(Roles = "TrainingStaff")]

		public ActionResult Edit(Course course)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			var courseInDb = _context.Courses.SingleOrDefault(p => p.Id == course.Id);

			if (courseInDb == null)
			{
				return HttpNotFound();
			}

			courseInDb.Name = course.Name;
			courseInDb.CategoryId = course.CategoryId;
			courseInDb.TopicId = course.TopicId;
			_context.SaveChanges();

			return RedirectToAction("Index");
		}
		public ActionResult Details(int id)
		{
			var courseInDb = _context.Courses.SingleOrDefault(p => p.Id == id);

			if (courseInDb == null)
			{
				return HttpNotFound();
			}

			return View(courseInDb);
		}
	}
}