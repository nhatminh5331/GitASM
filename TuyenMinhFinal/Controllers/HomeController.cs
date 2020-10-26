using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuyenMinhFinal.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Migrations;

namespace TuyenMinhFinal.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            ViewBag.Name = currentUser.Name;
            return View(currentUser);
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            if (currentUser == null)
            {
                return HttpNotFound();
            }
            return View(currentUser);
        }

        [HttpPost]
        public ActionResult Edit(ApplicationUser user)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());

            if (currentUser == null)
            {
                return View(user);
            }

            if (ModelState.IsValid)
            {
                currentUser.Name = user.Name;
                currentUser.PhoneNumber = user.PhoneNumber;
                currentUser.Email = user.Email;


                _context.Users.Add(currentUser);
                _context.SaveChanges();

                return RedirectToAction("About");
            }
            return View(user);

        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}