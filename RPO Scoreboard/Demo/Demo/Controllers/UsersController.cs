using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Demo.Models;
using Demo.Repositories;
using Demo.ViewModels;

namespace Demo.Controllers
{
    public class UsersController : Controller
    {
        private IRepository<UserProfile> _userProfileRepo;

        public UsersController()
        {
            _userProfileRepo = new BasicRepo<UserProfile>();
        }

        //
        // GET: /User/
        [Authorize(Roles=Util.ProjectRoles.ADMIN)]
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /User/Details/5
        [Authorize(Roles=Util.ProjectRoles.ADMIN)]
        public ActionResult Details(int id)
        {
            UserProfile profile = _userProfileRepo.Get(id);
            if (profile == null)
            {
                return new HttpNotFoundResult();
            }
            return View(createUserDetailsViewModel(profile));
        }

        [Authorize]
        public ActionResult Details()
        {
            string userName = Membership.GetUser().UserName;
            IQueryable<UserProfile> users = _userProfileRepo.GetAll();
            UserProfile profile = users.Where((x) => x.UserName == userName).FirstOrDefault();

            return View(createUserDetailsViewModel(profile));
        }

        private UserDetailsViewModel createUserDetailsViewModel(UserProfile userProfile)
        {
            UserDetailsViewModel viewModel = new UserDetailsViewModel();
            viewModel.UserName = userProfile.UserName;
            foreach (PointScore pointScore in userProfile.Score)
            {
                viewModel.Scores.Add(pointScore.PointPath.Name, pointScore.Score);
            }
            foreach (CompletedTask task in userProfile.CompletedTask)
            {
                if (task.Task.IsMilestone)
                {
                    viewModel.CompletedMilestones.Add(new UserDetialsMilestoneViewModel()
                    {
                        TaskID = task.ID,
                        Name = task.Task.Name,
                        PointsEarned = task.AwardedPoints,
                        CompletedOn = task.CompletedDate,
                        ImgLink = task.Task.IconLink
                    });
                }
                else
                {
                    viewModel.CompletedTask.Add(new UserDetialsTaskViewModel()
                    {
                        TaskID = task.ID,
                        Name = task.Task.Name,
                        PointsEarned = task.AwardedPoints,
                        CompletedOn = task.CompletedDate
                    });
                }
            }

            return viewModel;
        }

        //
        // GET: /User/Create
        [Authorize(Roles = Util.ProjectRoles.ADMIN)]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        [Authorize(Roles = Util.ProjectRoles.ADMIN)]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /User/Edit/5
        [Authorize(Roles = Util.ProjectRoles.ADMIN)]
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [Authorize(Roles = Util.ProjectRoles.ADMIN)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /User/Delete/5
        [Authorize(Roles = Util.ProjectRoles.ADMIN)]
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /User/Delete/5

        [HttpPost]
        [Authorize(Roles = Util.ProjectRoles.ADMIN)]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
