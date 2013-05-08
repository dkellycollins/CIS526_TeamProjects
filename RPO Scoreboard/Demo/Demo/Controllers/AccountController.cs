﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Profile;
using System.Web.Security;
using Demo.Filters;
using Demo.Models;
using Demo.Repositories;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;

namespace Demo.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {
        private IRepository<UserProfile> _userProfileRepo;

        public AccountController()
        {
            _userProfileRepo = new BasicRepo<UserProfile>();
        }

        //
        // GET: /Account/Login
        public ActionResult Login(string returnUrl)
        {
            //This is where we should check to see if the user has an account.
            UserProfile profile = _userProfileRepo.Get((x) => x.UserName == User.Identity.Name).FirstOrDefault();

            if(profile != null)
                return RedirectToAction("Index", "Scoreboard");
            return RedirectToAction("UserAgreement");
        }

        public ActionResult UserAgreement()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProfile()
        {
            UserProfile profile = new UserProfile()
            {
                ID = eid,
                UserName = User.Identity.Name,
                IsAdmin = false,
            };

            return RedirectToAction("Index", "Scoreboard");
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Scoreboard");
        }
    }
}
