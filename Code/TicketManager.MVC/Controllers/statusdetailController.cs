using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TicketManager.MVC.Models;

namespace TicketManager.MVC.Controllers
{
    public class statusdetailController : Controller
    {
        private TicketDBEntities db = new TicketDBEntities();

        // GET: statusdetail
        public ActionResult Index()
        {
            var statusdetail = db.statusdetail.Include(s => s.status);
            return View(statusdetail.ToList());
        }

        // GET: statusdetail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            statusdetail statusdetail = db.statusdetail.Find(id);
            if (statusdetail == null)
            {
                return HttpNotFound();
            }
            return View(statusdetail);
        }

        // GET: statusdetail/Create
        public ActionResult Create()
        {
            ViewBag.StatusId = new SelectList(db.status, "StatusId", "StatusName");
            return View();
        }

        // POST: statusdetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StatusDetailId,StatusId,StatusDetailName")] statusdetail statusdetail)
        {
            if (ModelState.IsValid)
            {
                db.statusdetail.Add(statusdetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StatusId = new SelectList(db.status, "StatusId", "StatusName", statusdetail.StatusId);
            return View(statusdetail);
        }

        // GET: statusdetail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            statusdetail statusdetail = db.statusdetail.Find(id);
            if (statusdetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.StatusId = new SelectList(db.status, "StatusId", "StatusName", statusdetail.StatusId);
            return View(statusdetail);
        }

        // POST: statusdetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StatusDetailId,StatusId,StatusDetailName")] statusdetail statusdetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statusdetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StatusId = new SelectList(db.status, "StatusId", "StatusName", statusdetail.StatusId);
            return View(statusdetail);
        }

        // GET: statusdetail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            statusdetail statusdetail = db.statusdetail.Find(id);
            if (statusdetail == null)
            {
                return HttpNotFound();
            }
            return View(statusdetail);
        }

        // POST: statusdetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            statusdetail statusdetail = db.statusdetail.Find(id);
            db.statusdetail.Remove(statusdetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
