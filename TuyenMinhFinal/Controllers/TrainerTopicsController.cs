using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuyenMinhFinal.Models;
using TuyenMinhFinal.ViewModels;
using System.Data.Entity.Migrations;
using System.Data.Entity;


namespace TuyenMinhFinal.Controllers
{
    public class TrainerTopicsController : Controller
    {
        private ApplicationDbContext _context;

        public TrainerTopicsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            if (User.IsInRole("TrainingStaff"))
            {
                var trainertopics = _context.TrainerTopics.Include(t => t.Topic).Include(t => t.Trainer).ToList();
                return View(trainertopics);
            }
            if (User.IsInRole("Trainer"))
            {
                var trainerId = User.Identity.GetUserId();
                var Res = _context.TrainerTopics.Where(e => e.TrainerId == trainerId).Include(t => t.Topic).ToList();
                return View(Res);
            }
            return View("Login");
        }

        public ActionResult Create()
        {
            //get trainer
            var role = (from r in _context.Roles where r.Name.Contains("Trainer") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            //get topic
            var topics = _context.Topics.ToList();

            var TrainerTopicVM = new TrainerTopicViewModel()
            {
                Topics = topics,
                Trainers = users,
                TrainerTopic = new TrainerTopic()
            };

            return View(TrainerTopicVM);
        }

        [HttpPost]
        public ActionResult Create(TrainerTopicViewModel model)
        {
            //get trainer
            var role = (from r in _context.Roles where r.Name.Contains("Trainer") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            //get topic
            var topics = _context.Topics.ToList();


            if (ModelState.IsValid)
            {
                _context.TrainerTopics.Add(model.TrainerTopic);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            var TrainerTopicVM = new TrainerTopicViewModel()
            {
                Topics = topics,
                Trainers = users,
                TrainerTopic = new TrainerTopic()
            };

            return View(TrainerTopicVM);
        }
        [HttpGet]
        [Authorize(Roles = "TrainingStaff")]
        public ActionResult Edit(int id)
        {
            var ttInDb = _context.TrainerTopics.SingleOrDefault(p => p.Id == id);
            if (ttInDb == null)
            {
                return HttpNotFound();
            }
            var viewModel = new TrainerTopicViewModel
            {
                TrainerTopic = ttInDb,
                Topics = _context.Topics.ToList(),

            };

            return View(viewModel);
        }
        [HttpPost]
        [Authorize(Roles = "TrainingStaff")]
        public ActionResult Edit(TrainerTopic trainerTopic)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            var ttInDb = _context.TrainerTopics.SingleOrDefault(p => p.Id == trainerTopic.Id);
            if (ttInDb == null)
            {
                return HttpNotFound();
            }
            ttInDb.TopicId = trainerTopic.TopicId;

            _context.SaveChanges();

            return RedirectToAction("Index", "TrainerTopics");
        }

        [Authorize(Roles = "TrainingStaff")]
        public ActionResult Delete(int id)
        {
            var trainertopicInDb = _context.TrainerTopics.SingleOrDefault(p => p.Id == id);

            if (trainertopicInDb == null)
            {
                return HttpNotFound();
            }
            _context.TrainerTopics.Remove(trainertopicInDb);
            _context.SaveChanges();

            return RedirectToAction("Index", "TrainerTopics");

        }
    }
}