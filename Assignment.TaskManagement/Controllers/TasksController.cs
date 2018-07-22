using System;
using System.Net;
using System.Web;
using System.Linq;
using System.Data;
using System.Web.Mvc;
using System.Data.Entity;
using System.Collections.Generic;
using Assignment.TaskManagement.Models;
using AnnalectIO.DomainService.Services;
using System.Threading.Tasks;
using AnnalectIO.DomainModel.Task;

namespace Assignment.TaskManagement.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            this._taskService = taskService;
        }

        //private TaskDbContext db = new TaskDbContext();

        //// GET: Tasks
        //public ActionResult Index()
        //{
        //    return View(db.Tasks.ToList());
        //}

        //// GET: Tasks/Details/5
        //public ActionResult Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Task task = db.Tasks.Find(id);
        //    if (task == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(task);
        //}

        //// GET: Tasks/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Tasks/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "id,Name,Description,DateCreated,DateUpdated")] Task task)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        task.id = Guid.NewGuid();
        //        task.DateCreated = DateTime.UtcNow;
        //        db.Tasks.Add(task);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(task);
        //}

        //// GET: Tasks/Edit/5
        //public ActionResult Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Task task = db.Tasks.Find(id);
        //    if (task == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(task);
        //}

        //// POST: Tasks/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "id,Name,Description,DateCreated,DateUpdated")] Task task)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        task.DateUpdated = DateTime.UtcNow;
        //        db.Entry(task).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(task);
        //}

        //// GET: Tasks/Delete/5
        //public ActionResult Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Task task = db.Tasks.Find(id);
        //    if (task == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(task);
        //}

        //// POST: Tasks/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(Guid id)
        //{
        //    Task task = db.Tasks.Find(id);
        //    db.Tasks.Remove(task);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}


        // GET: Tasks
        public async Task<ActionResult> Index()
        {
            var task = await this._taskService.GetAll();
            return View(task);
        }

        // GET: Tasks/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var task = await _taskService.GetById((Guid)id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TaskModel model)
        {
            if (ModelState.IsValid)
            {
                await _taskService.Create(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Tasks/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var task = await _taskService.GetById((Guid)id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,DateCreated,DateUpdated")] TaskModel model)
        {
            if (ModelState.IsValid)
            {
                await _taskService.Edit(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Tasks/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var task = await _taskService.GetById((Guid)id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            await _taskService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
