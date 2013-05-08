﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Demo.Models;
using Demo.Repositories;
using Demo.ViewModels;
using Demo.Filters;

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
        [CasAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /User/Details/5
        [CasAuthorize]
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
        public ActionResult Profile()
        {
            string userName = User.Identity.Name;
            UserProfile profile = _userProfileRepo.Get((x) => x.UserName == userName).FirstOrDefault();
            if (profile == null)
            {
                return new HttpNotFoundResult();
            }
            return View(createUserDetailsViewModel(profile));
        }

        private UserDetailsViewModel createUserDetailsViewModel(UserProfile userProfile)
        {
            UserDetailsViewModel viewModel = new UserDetailsViewModel();
            
            viewModel.UserName = userProfile.UserName;

            int totalScore = 0;
            viewModel.Scores = new Dictionary<string, int>();
            foreach (PointScore pointScore in userProfile.Score)
            {
                viewModel.Scores.Add(pointScore.PointPath.Name, pointScore.Score);
                totalScore += pointScore.Score;
            }
            viewModel.TotalScore = totalScore;

            viewModel.CompletedTask = new List<UserDetialsTaskViewModel>();
            viewModel.CompletedMilestones = new List<UserDetialsMilestoneViewModel>();
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
        // GET: /User/Edit/5
        [CasAuthorize]
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [CasAuthorize]
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
        [CasAuthorize]
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /User/Delete/5

        [HttpPost]
        [CasAuthorize]
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
