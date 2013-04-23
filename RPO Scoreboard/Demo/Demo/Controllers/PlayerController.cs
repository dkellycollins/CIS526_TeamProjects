using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo.Controllers
{
    /// <summary>
    /// Hanldes creating pages for players
    /// </summary>
    public class PlayerController : Controller
    {
        //
        // GET: /Player/

        public ActionResult Index()
        {
            return View();
        }

    }
}
