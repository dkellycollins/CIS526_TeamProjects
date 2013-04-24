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
    /// Hanldes creating pages for players
    /// </summary>
    public class UserController : Controller
    {
        //
        // GET: /Player/
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            throw new NotImplementedException();
        }
    }
}
