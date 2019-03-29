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
    public class tickethistoryController : Controller
    {
        private TicketDBEntities db = new TicketDBEntities();

        // GET: tickethistory

        public ActionResult BackToTicket(int? ticId)
        {
            try
            {
                // Pass the comment's article id to the read action
                return RedirectToAction("Details", "ticket", new { id = ticId });
            }
            catch (Exception e)
            {
                throw e;
            }
            // Something went wrong
        }

        public ActionResult Index(int? TicketOwner)
        {
            ViewBag.TicketId = ((TicketOwner == null) ? ViewBag.TicketId : TicketOwner);

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            ticket tic = db.ticket.Find(ViewBag.TicketId);
            if (tic == null)
            {
                return HttpNotFound();
            }

            //var tickethistory = db.tickethistory.Include(t => t.ticket).Include(t => t.user);

            var tickethistory = from t in db.tickethistory
                           select t;

            tickethistory = tickethistory.Where(t => t.TicketId == TicketOwner);

            tickethistory = tickethistory.OrderBy(t => t.TicketHistoryDate);

            return View(tickethistory.ToList());
        }

        // GET: tickethistory/Details/5
        public ActionResult Details(int? id, int? TicketOwner)
        {
            ViewBag.TicketOwner = ((TicketOwner == null) ? ViewBag.TicketId : TicketOwner);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tickethistory tickethistory = db.tickethistory.Find(id);
            if (tickethistory == null)
            {
                return HttpNotFound();
            }
            return View(tickethistory);
        }

        // GET: tickethistory/Create
        public ActionResult Create(int? TicketOwner)
        {
            ViewBag.TicketOwner = ((TicketOwner == null) ? ViewBag.TicketId : TicketOwner);

            ViewBag.TicketId = new SelectList(db.ticket, "TicketId", "TicketIssueSubject", TicketOwner);
            ViewBag.TicketHistoryUserId = new SelectList(db.user, "UserId", "UserName");

            ViewBag.StatusDetailId = new SelectList(db.statusdetail.Where(x => x.StatusId == 3).OrderBy(x => x.StatusDetailName), "StatusDetailId", "StatusDetailName", 0);

            return View();
        }

        // POST: tickethistory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TicketHistoryId,TicketId,TicketHistoryUserId,TicketHistoryDetail,TicketHistoryDate,StatusDetailId")] tickethistory tickethistory)
        {
            if (ModelState.IsValid)
            {
                if (tickethistory.ticket.StatusDetailId != 11)
                {
                    ticket tic = (from x in db.ticket
                                  where x.TicketId == tickethistory.TicketId
                                  select x).First();
                    tic.StatusId = 3;
                    tic.StatusDetailId = tickethistory.ticket.StatusDetailId;
                    db.SaveChanges();
                }

                db.tickethistory.Add(tickethistory);

                db.SaveChanges();
                return RedirectToAction("Index", "tickethistory", new { TicketOwner = tickethistory.TicketId });
            }

            ViewBag.TicketId = new SelectList(db.ticket, "TicketId", "TicketIssueSubject", tickethistory.TicketId);
            ViewBag.TicketHistoryUserId = new SelectList(db.user, "UserId", "UserName", tickethistory.TicketHistoryUserId);
            return View(tickethistory);
        }

        // GET: tickethistory/Edit/5
        public ActionResult Edit(int? id, int? TicketOwner)
        {
            ViewBag.TicketOwner = ((TicketOwner == null) ? ViewBag.TicketId : TicketOwner);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tickethistory tickethistory = db.tickethistory.Find(id);
            if (tickethistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.TicketId = new SelectList(db.ticket, "TicketId", "TicketIssueSubject", TicketOwner);
            ViewBag.TicketHistoryUserId = new SelectList(db.user, "UserId", "UserName", tickethistory.TicketHistoryUserId);

            return View(tickethistory);
        }

        // POST: tickethistory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TicketHistoryId,TicketId,TicketHistoryUserId,TicketHistoryDetail,TicketHistoryDate")] tickethistory tickethistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tickethistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "tickethistory", new { TicketOwner = tickethistory.TicketId });
            }
            ViewBag.TicketId = new SelectList(db.ticket, "TicketId", "TicketIssueSubject", tickethistory.TicketId);
            ViewBag.TicketHistoryUserId = new SelectList(db.user, "UserId", "UserName", tickethistory.TicketHistoryUserId);
            return View(tickethistory);
        }

        // GET: tickethistory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tickethistory tickethistory = db.tickethistory.Find(id);
            if (tickethistory == null)
            {
                return HttpNotFound();
            }
            return View(tickethistory);
        }

        // POST: tickethistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tickethistory tickethistory = db.tickethistory.Find(id);
            db.tickethistory.Remove(tickethistory);
            db.SaveChanges();
            return RedirectToAction("Index", "tickethistory", new { TicketOwner = tickethistory.TicketId });
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
