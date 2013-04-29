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
            List<UserProfile> users = _userRepo.GetAll().ToList();

            users.Sort(CompareByScore);

            return View(users);
        }

        //
        // GET: /Scoreboard/Details/{id}
        public ActionResult Details(int id = 0)
        {
            //UserProfile selectedUser = _userRepo.GetAll().Single(su=>su.ID == id);
            return View(_userRepo.Get(id));
            //return RedirectToAction("Index");
        }

        private static int CompareByScore(UserProfile x, UserProfile y)
        {
            return x.TotalScore.CompareTo(y.TotalScore) * -1;
        }
    }
}
