using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuyenMinhFinal.Models;
using TuyenMinhFinal.ViewModels;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;

namespace TuyenMinhFinal.Controllers
{
    public class TraineeCoursesController : Controller
    {
        private ApplicationDbContext _context;

        public TraineeCoursesController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            if (User.IsInRole("TrainingStaff"))
            {
                var traineecourses = _context.TraineeCourses.Include(t => t.Course).Include(t => t.Trainee).ToList();
                return View(traineecourses);
            }
            if (User.IsInRole("Trainee"))
            {
                var traineeId = User.Identity.GetUserId();
                var Res = _context.TraineeCourses.Where(e => e.TraineeId == traineeId).Include(t => t.Course).ToList();
                return View(Res);
            }
            return View("Login");
        }

        public ActionResult Create()
        {
            //get trainer
            var role = (from r in _context.Roles where r.Name.Contains("Trainee") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            //get topic

            var courses = _context.Courses.ToList();

            var TrainerTopicVM = new TraineeCourseViewModel()
            {
                Courses = courses,
                Trainees = users,
                TraineeCourse = new TraineeCourse()
            };

            return View(TrainerTopicVM);
        }

        [HttpPost]
        public ActionResult Create(TraineeCourseViewModel model)
        {
            //get trainer
            var role = (from r in _context.Roles where r.Name.Contains("Trainee") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            //get topic

            var courses = _context.Courses.ToList();


            if (ModelState.IsValid)
            {
                _context.TraineeCourses.Add(model.TraineeCourse);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            var TrainerTopicVM = new TraineeCourseViewModel()
            {
                Courses = courses,
                Trainees = users,
                TraineeCourse = new TraineeCourse()
            };

            return View(TrainerTopicVM);
        }
        [HttpGet]
        [Authorize(Roles = "TrainingStaff")]
        public ActionResult Edit(int id)
        {
            var tcInDb = _context.TraineeCourses.SingleOrDefault(p => p.Id == id);
            if (tcInDb == null)
            {
                return HttpNotFound();
            }
            var viewModel = new TraineeCourseViewModel
            {
                TraineeCourse = tcInDb,
                Courses = _context.Courses.ToList(),

            };

            return View(viewModel);
        }
        [HttpPost]
        [Authorize(Roles = "TrainingStaff")]
        public ActionResult Edit(TraineeCourse traineeCourse)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            var tcInDb = _context.TraineeCourses.SingleOrDefault(p => p.Id == traineeCourse.Id);
            if (tcInDb == null)
            {
                return HttpNotFound();
            }
            tcInDb.CourseId = traineeCourse.CourseId;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "TrainingStaff")]
        public ActionResult Delete(int id)
        {
            var traineecourseInDb = _context.TraineeCourses.SingleOrDefault(p => p.Id == id);

            if (traineecourseInDb == null)
            {
                return HttpNotFound();
            }
            _context.TraineeCourses.Remove(traineecourseInDb);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}