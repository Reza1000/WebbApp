using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OverBevakningApp.DAL;
using OverBevakningApp.Models;

namespace OverBevakningApp.Controllers
{
    public class RobotsController : Controller
    {
        private RobotContext db = new RobotContext();

        // GET: Robots
        public ActionResult Index() => View(db.Robots);

        // GET: Robots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Robot robot = db.Robots.Find(id);
            if (robot == null)
            {
                return HttpNotFound();
            }
            return View(robot);
        }

        // GET: Robots/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Robots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RobId,Beskrivning,IntPuls,ContactInfo")] Robot robot)
        {
            if (ModelState.IsValid)
            {
                db.Robots.Add(robot);
                db.SaveChanges();
                return RedirectToAction("AddLog", "RobotsLogs");
            }

            return View(robot);
        }

        // GET: Robots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Robot robot = db.Robots.Find(id);
            if (robot == null)
            {
                return HttpNotFound();
            }
            return View(robot);
        }

        // POST: Robots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RobId,Beskrivning,IntPuls,ContactInfo")] Robot robot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(robot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(robot);
        }

        // GET: Robots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Robot robot = db.Robots.Find(id);
            if (robot == null)
            {
                return HttpNotFound();
            }
            return View(robot);
        }

        // POST: Robots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Robot robot = db.Robots.Find(id);
            var robotsLog = db.RobotsLogs.Where(r => r.RobId == id).ToList();
            foreach (var item in robotsLog)
            {
                db.RobotsLogs.Remove(item);
                db.SaveChanges();
            }
            
            db.Robots.Remove(robot);
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
