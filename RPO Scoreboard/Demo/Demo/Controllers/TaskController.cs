using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Models;

namespace Demo.Controllers
{
    /// <summary>
    /// Handles creating pages for tasks.
    /// </summary>
    public class TaskController : Controller
    {
        //
        // GET: /Task/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            throw new NotImplementedException();
        }

        [Authorize]
        public ActionResult Create(Task item)
        {
            throw new NotImplementedException();
        }

        [Authorize]
        public ActionResult Update(Task item)
        {
            throw new NotImplementedException();
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
        }
    }
}
