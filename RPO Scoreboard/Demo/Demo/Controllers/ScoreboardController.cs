using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Models;
using Demo.Repositories;

namespace Demo.Controllers
{
    /// <summary>
    /// Handles creates pages for the score board.
    /// </summary>
    public class ScoreboardController : Controller
    {
        private IRepository<UserProfile> _userRepo;
        private IRepository<PointScore> _pointScoreRepo;

        public ScoreboardController()
        {
            _userRepo = new BasicRepo<UserProfile>();
            _pointScoreRepo = new BasicRepo<PointScore>();
        }

        //
        // GET: /Scoreboard/{pointType}
        public ActionResult Index(string pointType)
        {
            var users = _userRepo.GetAll();

            return View(users.ToList());
        }

        //
        // GET: /Scoreboard/Details/{id}
        public ActionResult Details(int id = 0)
        {
            //UserProfile selectedUser = _userRepo.GetAll().Single(su=>su.ID == id);
            return View(_userRepo.Get(id));
            //return RedirectToAction("Index");
        }
    }
}
