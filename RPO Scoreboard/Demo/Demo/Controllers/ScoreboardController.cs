using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Models;
using Demo.Repositories;
using System.ComponentModel.DataAnnotations;
using Demo.Filters;

namespace Demo.Models
{
    public class ScoreboardViewModel
    {
        [Display(Name="Scoreboard")]
        public List<ScoreboardRecord> Scoreboard { get; set; }

        [Display(Name = "Point Types")]
        public ScoreboardPointType ScoreboardPointTypes { get; set; }

        [Display(Name = "Page Number")]
        public int PageNumber { get; set; }
    }

    public class ScoreboardRecord
    {
        [Display(Name="User")]
        public UserProfile User { get; set; }

        [Display(Name="Score")]
        public int Score { get; set; }
    }

    public class ScoreboardJsonResult
    {
        [Display(Name = "Page Number")]
        public int PageNum { get; set; }

        [Display(Name = "Finished Loading")]
        public bool FinishedLoading { get; set; }

        [Display(Name = "Scoreboard")]
        public List<ScoreboardUser> Users { get; set; }

        [Display(Name = "Search Number")]
        public int UserIndex { get; set; }
    }

    public class ScoreboardUser
    {
        [Display(Name = "Username")]
        public String Username { get; set; }

        [Display(Name = "Score")]
        public int Score { get; set; }

        [Display(Name = "Milestones")]
        public List<ScoreboardMilestone> Milestones { get; set; }
    }

    public class ScoreboardMilestone
    {
        [Display(Name = "Name")]
        public String Name { get; set; }

        [Display(Name = "Description")]
        public String Description { get; set; }

        [Display(Name = "IconLink")]
        public String IconLink { get; set; }
    }

    public class ScoreboardPointType
    {
        [Display(Name="Point Types")]
        public List<PointType> PointTypes { get; set; }

        [Display(Name="Selected Point Type")]
        public string SelectedPointType { get; set; }
    }
}

namespace Demo.Controllers
{
    /// <summary>
    /// Handles creates pages for the score board.
    /// </summary>
    public class ScoreboardController : Controller
    {
        private int pageSize = 100;

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

            ViewBag.PageNumber = 0;
            ViewBag.PointType = pointType;

            return View(svm);
        }

        //POST
        public JsonResult GetNextPage(int currentPage, string pointType)
        {
            currentPage++;

            var profiles = _userRepo.GetAll().Where(ur => ur.IsAdmin == false);//.OrderBy(Skip(currentPage * pageSize).Take(pageSize);

            int userCount = profiles.Count();

            int maxPages = (userCount / pageSize);// +((userCount % pageSize) > 0 ? 1 : 0);

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

        public JsonResult SearchUser(int currentPage, string pointType, string userName)
        {
            var profiles = _userRepo.GetAll().Where(ur => ur.IsAdmin == false);

            int userCount = profiles.Count();

            int maxPages = (userCount / pageSize) + ((userCount % pageSize) > 0 ? 1 : 0);

            ScoreboardJsonResult sjr = new ScoreboardJsonResult();

            sjr.FinishedLoading = true;

            bool pointTypeExists = false;
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

            sjr.Users = recordList;

            sjr.Users.Sort(CompareByScore);

            int userIndex = sjr.Users.FindIndex(up => up.Username.Equals(userName));

            int userOnPage = (userIndex / pageSize);

            if (userOnPage > currentPage)
            {
                sjr.FinishedLoading = false;
                //Page that the user is on is not loaded
                sjr.Users = sjr.Users.Skip((currentPage + 1)* pageSize).Take((userOnPage - currentPage) * pageSize).ToList();
                sjr.PageNum = userOnPage;
            }
            else
            {
                sjr.Users = sjr.Users.Take(0).ToList();
                sjr.PageNum = currentPage;
            }

            sjr.UserIndex = userIndex;

            return Json(sjr, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Scoreboard/Details/{id}
        public ActionResult Details(int id = 0)
        {
            //UserProfile selectedUser = _userRepo.GetAll().Single(su=>su.ID == id);
            return View(_userRepo.Get(id));
            //return RedirectToAction("Index");
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
