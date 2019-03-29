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
    public class contactController : Controller
    {
        private TicketDBEntities db = new TicketDBEntities();

        // GET: contact
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Fandrade TecInfo Ticket Manager!";
            ViewData["Contacts"] = GetContacts();
            return View();
        }

        // GET: contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contact contact = db.contact.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: contact/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.customer.OrderBy(o => o.CustomerAbrev), "CustomerId", "CustomerAbrev");
            return View();
        }

        // POST: contact/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContactId,CustomerId,ContactName,ContactEmail")] contact contact)
        {
            if (ModelState.IsValid)
            {
                db.contact.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.customer.OrderBy(o => o.CustomerAbrev), "CustomerId", "CustomerName", contact.CustomerId);
            return View(contact);
        }

        // GET: contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contact contact = db.contact.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.customer.OrderBy(o => o.CustomerAbrev), "CustomerId", "CustomerName", contact.CustomerId);
            return View(contact);
        }

        // POST: contact/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContactId,CustomerId,ContactName,ContactEmail")] contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.customer.OrderBy(o => o.CustomerAbrev), "CustomerId", "CustomerName", contact.CustomerId);
            return View(contact);
        }

        // GET: contact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contact contact = db.contact.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            contact contact = db.contact.Find(id);
            db.contact.Remove(contact);
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

        private List<contact> GetContacts()
        {
            var con = from s in db.contact orderby s.ContactName ascending select s;
            return con.ToList();
        }
    }
}
