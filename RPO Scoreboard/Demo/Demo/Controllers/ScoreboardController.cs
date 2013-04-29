using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Models;
using Demo.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class ScoreboardViewModel
    {
        [Display(Name="Scoreboard")]
        public List<UserProfile> Scoreboard { get; set; }
        [Display(Name="Point Types")]
        public List<PointType> PointTypes { get; set; }
    }
}

namespace Demo.Controllers
{
    /// <summary>
    /// Handles creates pages for the score board.
    /// </summary>
    public class ScoreboardController : Controller
    {
        private IRepository<UserProfile> _userRepo;
        private IRepository<PointScore> _pointScoreRepo;
        private IRepository<PointType> _pointTypeRepo;

        public ScoreboardController()
        {
            _userRepo = new BasicRepo<UserProfile>();
            _pointScoreRepo = new BasicRepo<PointScore>();
            _pointTypeRepo = new BasicRepo<PointType>();
        }

        //
        // GET: /Scoreboard/{pointType}
        public ActionResult Index(string pointType)
        {
            //List<UserProfile> users = _userRepo.GetAll().ToList();

            //users.Sort(CompareByScore);

            ScoreboardViewModel svm = new ScoreboardViewModel()
            {
                Scoreboard = _userRepo.GetAll().ToList(),
                PointTypes = _pointTypeRepo.GetAll().ToList()
            };

            svm.Scoreboard.Sort(CompareByScore);

            return View(svm);
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
