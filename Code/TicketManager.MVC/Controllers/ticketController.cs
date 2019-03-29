using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Dynamic;
using TicketManager.MVC.Models;

namespace TicketManager.MVC.Controllers
{
    public class ticketController : Controller
    {
        private TicketDBEntities db = new TicketDBEntities();

        public int currentStatusId { get; set; }

        #region "History Actions"
        public ActionResult CreateHistory(int? ticId)
        {
            try
            {
                return RedirectToAction("Create", "tickethistory", new { TicketOwner = ticId });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ActionResult EditHistory(int? hisId, int? ticId)
        {
            try
            {
                return RedirectToAction("Edit", "tickethistory", new { id = hisId, TicketOwner = ticId });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ActionResult DetailsHistory(int? hisId, int? ticId)
        {
            try
            {
                return RedirectToAction("Details", "tickethistory", new { id = hisId, TicketOwner = ticId });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        public ActionResult Index(bool? bFinalizados)
        {
            ViewBag.Message = "Welcome to Fandrade TecInfo Ticket Manager!";
            ViewData["Checks"] = GetChecks();
            ViewData["Tickets"] = GetTickets(bFinalizados == null ? false : bFinalizados.GetValueOrDefault());
            ViewBag.Finalizados = (bFinalizados == null ? false : bFinalizados.GetValueOrDefault());
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ticket ticket = db.ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewData["Histories"] = GetHistories(id);
            ViewData["Tickets"] = ticket;
            return View();

        }

        // GET: ticket/Create
        public ActionResult Create()
        {
            ViewBag.ContactId = new SelectList(db.contact.OrderBy(o => o.ContactName), "ContactId", "ContactName");
            ViewBag.CustomerId = new SelectList(db.customer.OrderBy(o => o.CustomerAbrev), "CustomerId", "CustomerAbrev");
            ViewBag.StatusId = new SelectList(db.status.OrderBy(o => o.StatusName), "StatusId", "StatusName");
            ViewBag.StatusDetailId = new SelectList(db.statusdetail.OrderBy(o => o.StatusDetailName), "StatusDetailId", "StatusDetailName");
            ViewBag.CreateUserid = new SelectList(db.user.OrderBy(o => o.UserName), "UserId", "UserName");
            ViewBag.RespUserId = new SelectList(db.user.OrderBy(o => o.UserName), "UserId", "UserName");
            return View();
        }

        // POST: ticket/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TicketId,CustomerId,ContactId,CreateUserid,StatusId,StatusDetailId,TicketDate,TicketIssueSubject,TicketIssueDescription,RespUserId")] ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.ticket.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContactId = new SelectList(db.contact.OrderBy(o => o.ContactName), "ContactId", "ContactName", ticket.ContactId);
            ViewBag.CustomerId = new SelectList(db.customer.OrderBy(o => o.CustomerName), "CustomerId", "CustomerName", ticket.CustomerId);
            ViewBag.StatusId = new SelectList(db.status.OrderBy(o => o.StatusName), "StatusId", "StatusName", ticket.StatusId);
            ViewBag.StatusDetailId = new SelectList(db.statusdetail.OrderBy(o => o.StatusDetailName), "StatusDetailId", "StatusDetailName", ticket.StatusDetailId);
            ViewBag.CreateUserid = new SelectList(db.user.OrderBy(o => o.UserName), "UserId", "UserName", ticket.CreateUserid);
            ViewBag.RespUserId = new SelectList(db.user.OrderBy(o => o.UserName), "UserId", "UserName", ticket.RespUserId);
            return View(ticket);
        }

        // GET: ticket/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ticket ticket = db.ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactId = new SelectList(db.contact, "ContactId", "ContactName", ticket.ContactId);
            ViewBag.CustomerId = new SelectList(db.customer.OrderBy(o => o.CustomerAbrev), "CustomerId", "CustomerName", ticket.CustomerId);
            ViewBag.StatusId = new SelectList(db.status, "StatusId", "StatusName", ticket.StatusId);
            ViewBag.StatusDetailId = new SelectList(db.statusdetail, "StatusDetailId", "StatusDetailName", ticket.StatusDetailId);
            ViewBag.CreateUserid = new SelectList(db.user, "UserId", "UserName", ticket.CreateUserid);
            ViewBag.RespUserId = new SelectList(db.user, "UserId", "UserName", ticket.RespUserId);

            ViewData["currentStatusId"] = ticket.StatusId;
            return View(ticket);
        }

        // POST: ticket/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TicketId,CustomerId,ContactId,CreateUserid,StatusId,StatusDetailId,TicketDate,TicketIssueSubject,TicketIssueDescription,RespUserId,OutlookEntryId")] ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                this.UpdateMailItem(ref ticket);
                return RedirectToAction("Index");
            }
            ViewBag.ContactId = new SelectList(db.contact, "ContactId", "ContactName", ticket.ContactId);
            ViewBag.CustomerId = new SelectList(db.customer.OrderBy(o => o.CustomerAbrev), "CustomerId", "CustomerName", ticket.CustomerId);
            ViewBag.StatusId = new SelectList(db.status, "StatusId", "StatusName", ticket.StatusId);
            ViewBag.StatusDetailId = new SelectList(db.statusdetail, "StatusDetailId", "StatusDetailName", ticket.StatusDetailId);
            ViewBag.CreateUserid = new SelectList(db.user, "UserId", "UserName", ticket.CreateUserid);
            ViewBag.RespUserId = new SelectList(db.user, "UserId", "UserName", ticket.RespUserId);
            return View(ticket);
        }

        // GET: ticket/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ticket ticket = db.ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: ticket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ticket ticket = db.ticket.Find(id);
            db.ticket.Remove(ticket);
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

        #region "Multiple models One view" 
        private List<Check> GetChecks()
        {
            List<Check> checks = new List<Check>();
            foreach (var item in db.status.ToList())
            {
                checks.Add(new Check { Id = item.StatusId, Checked = true, Name = item.StatusName });
            }
            return checks;
        }
        private List<ticket> GetTickets(bool bFinalizados)
        {
            var tic = from s in db.ticket select s;
            if (!bFinalizados)
                tic = tic.Where(s => s.StatusId == 1 || s.StatusId == 2);
            else

                tic = tic.Where(s => s.StatusId == 3);
            return tic.ToList();
        }

        private List<tickethistory> GetHistories(int? id)
        {
            var his = from s in db.tickethistory select s;
            his = his.Where(s => s.TicketId == id).OrderByDescending(s => s.TicketHistoryDate);
            return his.ToList();
        }
        #endregion

        #region "Parent Status / Child StatusDetail" 
        public ActionResult LoadStatus()
        {
            ViewData["statusList"] = db.status.ToList();
            return View();
        }
        public JsonResult GetStatusDetail(int id)
        {
            var det = from s in db.statusdetail select s;
            det = det.Where(s => s.StatusId == id).OrderBy(o => o.StatusDetailName); ;
            return Json(new SelectList(det.ToList(), "StatusDetailId", "StatusDetailName"));
        }
        #endregion
        
        #region "Parent Customer / Child Contact" 
        public ActionResult LoadCustomer()
        {
            ViewData["customerList"] = db.customer.ToList();
            return View();
        }
        public JsonResult GetContact(int id)
        {
            var con = from s in db.contact select s;
            con = con.Where(s => s.CustomerId == id).OrderBy(o => o.ContactName);
            return Json(new SelectList(con.ToList(), "ContactId", "ContactName"));
        }
        #endregion

        private void UpdateMailItem(ref ticket tic)
        {
            if (string.IsNullOrEmpty(tic.OutlookEntryId)) return;

            // Implementar caso não tenha mudado status, fazer nada.

            TicketManager.BusinessRules.OutlookManager outlook = new TicketManager.BusinessRules.OutlookManager();

            int statusId = tic.StatusId;
            var sta = from s in db.status select s;
            sta = sta.Where(s => s.StatusId == statusId);

            int customerId = tic.CustomerId;
            var cus = from c in db.customer select c;
            cus = cus.Where(c => c.CustomerId == customerId);

            string category = "#; " + sta.First().StatusName.Substring(3, sta.First().StatusName.Length - 3) + "; " + cus.First().CustomerAbrev;

            try
            {
                outlook.AtualizarMailItem(category, tic.OutlookEntryId);
            }
            catch (Exception ex)
            {
                string cleanMessage = ex.Message;
                string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');</script>";
                Response.Write(script);
            }
        }
    }
}
