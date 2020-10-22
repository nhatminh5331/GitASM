using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuyenMinhFinal.Models;
using TuyenMinhFinal.ViewModels;

namespace TuyenMinhFinal.Controllers
{
    public class ManagerStaffViewModelsController : Controller
    {
        ApplicationDbContext _context;
        public ManagerStaffViewModelsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: ManagerStaffViewModels
        public ActionResult Index()
        {
            var role = (from r in _context.Roles where r.Name.Contains("Trainee") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            var userVM = users.Select(user => new ManagerStaffViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                RoleName = "Trainee",
                UserId = user.Id
            }).ToList();


            var role2 = (from r in _context.Roles where r.Name.Contains("Trainer") select r).FirstOrDefault();
            var admins = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role2.Id)).ToList();

            var adminVM = admins.Select(user => new ManagerStaffViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                RoleName = "Trainer",
                UserId = user.Id
            }).ToList();


            var model = new ManagerStaffViewModel { Trainee = userVM, Trainer = adminVM };
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "TrainingStaff")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var appUser = _context.Users.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }

        [HttpPost]
        [Authorize(Roles = "TrainingStaff")]
        public ActionResult Edit(ApplicationUser user)
        {
            var userInDb = _context.Users.Find(user.Id);

            if (userInDb == null)
            {
                return View(user);
            }

            if (ModelState.IsValid)
            {
                userInDb.Name = user.Name;
                userInDb.UserName = user.UserName;
                userInDb.PhoneNumber = user.PhoneNumber;
                userInDb.Email = user.Email;


                _context.Users.Add(userInDb);
                _context.SaveChanges();

                return RedirectToAction("Index", "ManagerStaffViewModels");
            }
            return View(user);

        }

        [Authorize(Roles = "TrainingStaff")]
        public ActionResult Delete(string id)
        {
            var userInDb = _context.Users.SingleOrDefault(p => p.Id == id);

            if (userInDb == null)
            {
                return HttpNotFound();
            }
            _context.Users.Remove(userInDb);
            _context.SaveChanges();

            return RedirectToAction("Index", "ManagerStaffViewModels");

        }

    }
}