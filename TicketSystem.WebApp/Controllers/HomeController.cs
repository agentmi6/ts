using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketSystem.Data;

namespace TicketSystem.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var tickets = db.Tickets.Take(9).ToList();               
            return View(tickets);
        }
       
    }
}