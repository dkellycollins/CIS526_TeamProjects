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
            var pointScores = from u in _pointScoreRepo.GetAll() select u;

            if (string.IsNullOrEmpty(pointType))
                pointScores.Where(s => s.PointPath.Name == "Total");
            else
                pointScores.Where(s => s.PointPath.Name == pointType);

            return View(pointScores.ToList());
        }
    }
}
