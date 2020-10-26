using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TuyenMinhFinal.Models;
using TuyenMinhFinal.ViewModels;
using static TuyenMinhFinal.Controllers.AccountController;
using static TuyenMinhFinal.Controllers.ManageController;

namespace TuyenMinhFinal.Controllers
{
    public class ManageUsersController : Controller
    {
        private ApplicationDbContext _context;
        public ManageUsersController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: ManageUsers
        public ActionResult UsersWithRoles()
        {
            var usersWithRoles = (from user in _context.Users 
                                  select new { UserId = user.Id,
                                   Name = user.Name,
                                   Username = user.UserName,
                                   Emailaddress = user.Email,
                                   Password = user.PasswordHash,
                                   RoleNames = (from userRole in user.Roles join role in _context.Roles on userRole.RoleId
                                                equals role.Id
                                                select role.Name).ToList()
                                  }).ToList().Select(p => new UsersInRole()
                                  {
                                      UserId = p.UserId,
                                      Name = p.Name,
                                      Username = p.Username,
                                      Email = p.Emailaddress,
                                      Role = string.Join(",", p.RoleNames)
                                  });

            return View(usersWithRoles);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

                return RedirectToAction("UsersWithRoles");
            }
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            var userInDb = _context.Users.SingleOrDefault(p => p.Id == id);

            if (userInDb == null)
            {
                return HttpNotFound();
            }
            _context.Users.Remove(userInDb);
            _context.SaveChanges();

            return RedirectToAction("UsersWithRoles");

        }
    }
}