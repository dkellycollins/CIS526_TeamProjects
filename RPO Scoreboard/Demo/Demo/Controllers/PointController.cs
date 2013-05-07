using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Models;
using Demo.Repositories;

namespace Demo.Controllers
{
    [Authorize(Roles = Util.ProjectRoles.ADMIN)]
    public class PointController : Controller
    {
        private IRepository<PointType> _pointTypeRepo;

        public PointController()
        {
            _pointTypeRepo = new BasicRepo<PointType>();
        }

        //
        // GET: /Point/
        public ActionResult Index()
        {
            return View(_pointTypeRepo.GetAll());
        }

        //
        // GET: /Point/Details/{id}
        public ActionResult Details(int id = 0)
        {
            return View(_pointTypeRepo.Get(id));
        }

        //
        // GET: /Point/Create/
        public ActionResult Create()
        {
            return View();
        }
        
        //
        // POST: /Point/Create/
        [HttpPost]
        public ActionResult Create(PointType item)
        {
            if (ModelState.IsValid)
            {
                _pointTypeRepo.Create(item);
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /Point/Update/{id}
        public ActionResult Update(int id)
        {
            return View(_pointTypeRepo.Get(id));
        }

        //
        // POST: /Point/Update/
        [HttpPost]
        public ActionResult Update(PointType item)
        {
            if (ModelState.IsValid)
            {
                _pointTypeRepo.Create(item);
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /Point/Remove/{id}
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(_pointTypeRepo.Get(id));
        }

        //
        // POST: /Point/Remove/{id}
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            _pointTypeRepo.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
