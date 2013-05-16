using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Demo.Encryption.RSA;
using Demo.Filters;
using Demo.Models;
using Demo.Repositories;
using Demo.ViewModels;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using WebMatrix.WebData;

namespace Demo.Controllers
{
    /// <summary>
    /// Handles creating pages for tasks.
    /// </summary>
    public class TaskController : Controller
    {
        private IRepository<UserProfile> _userRepo;
        private IRepository<Task> _taskRepo;

        public TaskController()
        {
            _taskRepo = new BasicRepo<Task>();
            _userRepo = new BasicRepo<UserProfile>();
        }

        //
        // GET: /Task/
        public ActionResult Index()
        {
            TaskViewModel tvm = new TaskViewModel()
            {
                MileStones = _taskRepo.GetAll().Where(tr => tr.IsMilestone == true).ToList(),
                Tasks = _taskRepo.GetAll().Where(tr=>tr.IsMilestone == false).ToList()
            };

            return View(tvm);
        }

        //
        // GET: /Task/Detials/{id}
        public ActionResult Details(int id)
        {
            UserProfile userProfile = _userRepo.Get((u) => u.UserName == WebSecurity.CurrentUserName).FirstOrDefault();
            if (userProfile != null && userProfile.IsAdmin)
                return View(_taskRepo.Get(id));
            else
                return RedirectToAction("CompleteTask", new { id = id });
        }

        //Admin Task
        // GET: /Task/Create/
        [CasAuthorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //Admin Task
        // POST: /Task/Create/
        [CasAuthorize]
        [HttpPost, ActionName("Create")]
        public ActionResult Create(Task item)
        {
            if(ModelState.IsValid)
            {
                item.Token = new Guid().ToString();
                _taskRepo.Create(item);
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /Task/Update/
        [CasAuthorize]
        [HttpGet]
        public ActionResult Update(int id)
        {
            return View(_taskRepo.Get(id));
        }

        //
        // POST: /Task/Update/
        [CasAuthorize]
        public ActionResult Update(Task item)
        {
            if(ModelState.IsValid)
            {
                _taskRepo.Create(item);
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /Task/Delete/{id}
        [HttpGet]
        [CasAuthorize]
        public ActionResult Delete(int id)
        {
            return View(_taskRepo.Get(id));
        }

        //
        // POST: /Task/Delete/{id}       
        [CasAuthorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteComfirmed(int id)
        {
            _taskRepo.Delete(id);

            return RedirectToAction("Index");
        }

        //
        // GET: /Task/CompleteTask/{id}
        [HttpGet]
        public ActionResult CompleteTask(int id)
        {
            Task task = _taskRepo.Get(id);
            if (task == null)
                return new HttpNotFoundResult();
            return View(createTaskViewModel(task));
        }

        //
        // POST: /Task/CompleteTask/
        [HttpPost]
        public ActionResult CompleteTask(FormCollection formCollection)
        {
            string status = null;
            
            //Find user and task.
            string userID = formCollection["UserID"];
            string taskToken = formCollection["TaskToken"];
            UserProfile user = _userRepo.Get((x) => x.UserName == userID).FirstOrDefault();
            Task task = _taskRepo.Get((t) => t.Token == taskToken).FirstOrDefault();
            
            status = commonCompleteTask(user, task, formCollection["Solution"]);

            TempData["StatusMessage"] = status;
            return RedirectToAction("Index", "Scoreboard");
        }

        [HttpPost]
        public ActionResult CompleteTaskExternal(byte[] data)
        {
            //RsaDecryptor decryptor = new RsaDecryptor();
            RsaDecryptor decryptor = null;
            TaskCompletePacket packet = new TaskCompletePacket(decryptor.Decrypt(data));
            UserProfile user = _userRepo.Get((u) => u.UserName == packet.UserID).FirstOrDefault();
            Task task = _taskRepo.Get((t) => t.Token == packet.TaskToken).FirstOrDefault();

            commonCompleteTask(user, task);

            return new HttpStatusCodeResult(204); //Means that we accepted the request.
        }

        [HttpPost]
        public ActionResult CompleteTaskQR(string taskToken)
        {
            UserProfile user = _userRepo.Get(WebSecurity.CurrentUserId);
            Task task = _taskRepo.Get((t) => t.Token == taskToken).FirstOrDefault();

            commonCompleteTask(user, task);

            return new HttpStatusCodeResult(204); //Means that we accepted the request.
        }

        [CasAdminAuthorize]
        public ActionResult GenerateQRCode(int id)
        {
            Task task = _taskRepo.Get(id);
            if(task == null)
                return new HttpNotFoundResult();

            QrCode qrCode;
            QrEncoder encoder = new QrEncoder();
            encoder.TryEncode(task.Token, out qrCode);

            GraphicsRenderer gRenderer = new GraphicsRenderer(
                new FixedModuleSize(2, QuietZoneModules.Two),
                Brushes.Black, Brushes.White);

            MemoryStream ms = new MemoryStream();
            gRenderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);

            return File(ms.ToArray(), "image/png");
        }

        #region Private Members

        private TaskCompleteViewModel createTaskViewModel(Task task)
        {
            TaskCompleteViewModel tcvm = new TaskCompleteViewModel()
            {
                TaskName = task.Name,
                TaskDescription = task.Description,
                TaskToken = task.Token
            };

            return tcvm;
        }

        //Verifies that the fields are valid and returns the status.
        private string commonCompleteTask(UserProfile user, Task task, string solution = null)
        {
            string status;
            if (user == null)
            { //Ensure we have a user.
                status = "User not found";
            }
            else if (task == null)
            { //Ensure we have a task.
                status = "Task not found";
            }
            else if (DateTime.Now < task.StartTime || DateTime.Now > task.EndTime)
            { //Verify that this task can be completed.
                status = "Task is not availible at this time";
            }
            else
            { //Complete the task.
                if (!string.IsNullOrEmpty(task.Solution))
                {
                    if (solution == task.Solution)
                    {
                        addTaskToUser(user, task);
                        status = "Correct!";
                    }
                    else
                    {
                        status = "Incorrect solution.";
                    }
                }
                else
                {
                    addTaskToUser(user, task);
                    status = "Task Completed";
                }
            }

            return status;
        }

        //Adds a task to the user.
        private void addTaskToUser(UserProfile user, Task task)
        {
            CompletedTask cTask = new CompletedTask()
            {
                Task = task,
                TaskID = task.ID,
                UserProfile = user,
                UserProfileID = user.ID,
                CompletedDate = DateTime.Now,
            };

            if (task.CompletedBy.Count < task.MaxBonusAwards)
                cTask.AwardedPoints = task.Points + task.BonusPoints;
            else
                cTask.AwardedPoints = task.Points;

            user.CompletedTask.Add(cTask);
            task.CompletedBy.Add(cTask);
            _taskRepo.Update(task);
            _userRepo.Update(user);
        }

        #endregion
    }
}
