using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo.Controllers
{
    /// <summary>
    /// Handles creates pages for the score board.
    /// </summary>
    public class ScoreController : Controller
    {
        //
        // GET: /Score/{path}
        public ActionResult Index(string path)
        {
            //Query the database for point path mathching the given path.
            return View();
        }

    }
}
