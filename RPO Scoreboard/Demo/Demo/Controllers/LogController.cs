using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Models;
using Demo.Repositories;

namespace Demo.Controllers
{
    public class LogController : Controller
    {
        private IRepository<Log> _logRepo;

        /// <summary>
        /// So the other controller can access the same instance.
        /// </summary>
        internal static LogController Instance = new LogController();

        public LogController()
        {
            _logRepo = new BasicRepo<Log>();
        }

        //
        // GET: /Log/
        [Authorize(Roles="admin")]
        public ActionResult Index()
        {
            return View(_logRepo.GetAll().ToList());
        }

        internal void Log(string userName, string action, Controller sender)
        {
            Log log = new Log()
            {
                UserName = userName,
                Action = action,
                Time = DateTime.Now,
                Controller = sender.GetType().Namespace,
            };
            _logRepo.Create(log);
        }
    }
}
