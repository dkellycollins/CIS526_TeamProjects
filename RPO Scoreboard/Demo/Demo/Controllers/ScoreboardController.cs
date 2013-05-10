using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Models;
using Demo.Repositories;
using System.ComponentModel.DataAnnotations;
using Demo.Filters;
using Demo.ViewModels;

namespace Demo.Controllers
{
    /// <summary>
    /// Handles creates pages for the score board.
    /// </summary>
    public class ScoreboardController : Controller
    {
        private int pageSize = 100;

        private IRepository<UserProfile> _userRepo;
        private IRepository<PointType> _pointTypeRepo;

        public ScoreboardController()
        {
            _userRepo = new BasicRepo<UserProfile>();
            _pointTypeRepo = new BasicRepo<PointType>();
        }

        //
        // GET: /Scoreboard/{pointType}
        public ActionResult Index(string pointType)
        {
            bool pointTypeExists = false;

            ScoreboardViewModel svm = new ScoreboardViewModel()
            {
                Scoreboard = new List<ScoreboardRecord>(),
                ScoreboardPointTypes = new ScoreboardPointType()
                {
                    PointTypes = _pointTypeRepo.GetAll().ToList(),
                    SelectedPointType = pointType
                },
                PageNumber = 0
            };

            foreach (PointType pt in svm.ScoreboardPointTypes.PointTypes)
            {
                pointTypeExists = (pt.Name == pointType);

                if (pointTypeExists) break;
            }

            foreach (var user in _userRepo.GetAll().Where(ur => ur.IsAdmin == false))
            {
                svm.Scoreboard.Add(new ScoreboardRecord()
                {
                    User = user,
                    Score = pointTypeExists ? user.ScoreFor(pointType) : user.TotalScore
                });
            }

            svm.Scoreboard.Sort(CompareByScore);

            svm.Scoreboard = svm.Scoreboard.Take(pageSize).ToList();

            return View(svm);
        }

        //POST
        
        public JsonResult GetNextPage(int currentPage, string pointType)
        {
            currentPage++;

            var profiles = _userRepo.GetAll().Where(ur => ur.IsAdmin == false);//.OrderBy(Skip(currentPage * pageSize).Take(pageSize);

            int userCount = profiles.Count();

            int maxPages = (userCount / pageSize) + ((userCount % pageSize) > 0 ? 1 : 0);

            bool pointTypeExists = false;

            ScoreboardJsonResult sjr = new ScoreboardJsonResult()
            {
                PageNum = currentPage,
                FinishedLoading = false
            };

            if (currentPage >= maxPages) sjr.FinishedLoading = true;

            foreach (PointType pt in _pointTypeRepo.GetAll())
            {
                pointTypeExists = (pt.Name == pointType);

                if (pointTypeExists) break;
            }

            List<ScoreboardUser> recordList = new List<ScoreboardUser>();
            foreach (var user in profiles)
            {
                ScoreboardUser newUser = new ScoreboardUser()
                {
                    Username = user.UserName,
                    Score = pointTypeExists ? user.ScoreFor(pointType) : user.TotalScore,
                    Milestones = new List<ScoreboardMilestone>()
                };

                foreach (CompletedTask ct in user.CompletedTask)
                {
                    if (ct.Task.IsMilestone)
                    {
                        newUser.Milestones.Add(new ScoreboardMilestone()
                        {
                            Name = ct.Task.Name,
                            Description = ct.Task.Description,
                            IconLink = ct.Task.IconLink
                        });
                    }
                }

                recordList.Add(newUser);
            }


            recordList.Sort(CompareByScore);

            recordList = recordList.Skip(currentPage * pageSize).Take(pageSize).ToList();

            sjr.Users = recordList;

            return Json(sjr, JsonRequestBehavior.AllowGet);
        }
        

        //
        // GET: /Scoreboard/Details/{id}
        public ActionResult Details(int id = 0)
        {
            return View(_userRepo.Get(id));
        }

        private static int CompareByScore(ScoreboardRecord x, ScoreboardRecord y)
        {
            return x.Score.CompareTo(y.Score) * -1;
        }

        private static int CompareByScore(ScoreboardUser x, ScoreboardUser y)
        {
            return x.Score.CompareTo(y.Score) * -1;
        }
    }
}
