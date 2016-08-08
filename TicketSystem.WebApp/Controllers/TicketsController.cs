using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TicketSystem.Data;
using TicketSystem.Domain;
using PagedList;

namespace TicketSystem.WebApp.Controllers
{
    [Authorize(Roles = "user")]
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult AllTickets(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SortByTitle = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.SortByCategory = String.IsNullOrEmpty(sortOrder) ? "cat_desc" : "";
            ViewBag.SortByPriority = String.IsNullOrEmpty(sortOrder) ? "priority_desc" : "";
            ViewBag.SortByAuthor = String.IsNullOrEmpty(sortOrder) ? "author_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;


            var tickets =
                from t in db.Tickets
                select t;

            if (!string.IsNullOrEmpty(searchString))
            {
                tickets =
                    tickets.
                    Where(x => x.TicketTitle.Contains(searchString)
                    || x.Category.Name.Contains(searchString)
                    || x.ApplicationUser.UserName.Contains(searchString)
                    || x.Priority.PriorityStatus.Contains(searchString));
            }
           
            switch (sortOrder)
            {                
                case "title_desc":
                    tickets = tickets.OrderByDescending(x => x.TicketTitle);
                    break;
                case "cat_desc":
                    tickets = tickets.OrderByDescending(x => x.Category.Name);
                    break;
                case "priority_desc":
                    tickets=tickets.OrderBy(x=>x.Priority.PriorityStatus);
                    break;
                default:
                    tickets = tickets.OrderBy(x => x.TicketTitle);
                    break;
            }

            int pageSize = 4;
            int pageNumber = (page ?? 1);
           
            return View(tickets.ToPagedList(pageNumber,pageSize));
        }


        public ActionResult Index()
        {
            var currentUser = User.Identity.GetUserId();
            var myTickets = db.Tickets.Where(x => x.ApplicationUserID == currentUser);

            return View(myTickets.ToList());
        }

        [AllowAnonymous]
        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            ViewBag.PriorityID = new SelectList(db.Priorities, "PriorityID", "PriorityStatus");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TicketID,TicketTitle,ScreenshotURL,Description,ApplicationUserID,CategoryID,PriorityID")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {               
                ticket.ApplicationUserID = User.Identity.GetUserId();
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", ticket.CategoryID);
            ViewBag.PriorityID = new SelectList(db.Priorities, "PriorityID", "PriorityStatus", ticket.PriorityID);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", ticket.CategoryID);
            ViewBag.PriorityID = new SelectList(db.Priorities, "PriorityID", "PriorityStatus", ticket.PriorityID);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TicketID,TicketTitle,ScreenshotURL,Description,ApplicationUserID,CategoryID,PriorityID")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.ApplicationUserID = User.Identity.GetUserId();
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", ticket.CategoryID);
            ViewBag.PriorityID = new SelectList(db.Priorities, "PriorityID", "PriorityStatus", ticket.PriorityID);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
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
