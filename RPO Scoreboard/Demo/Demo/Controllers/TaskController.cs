using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

    }
}
