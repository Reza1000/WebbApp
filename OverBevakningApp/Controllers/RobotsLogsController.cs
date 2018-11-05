using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using OverBevakningApp.DAL;
using OverBevakningApp.Models;

namespace OverBevakningApp.Controllers
{
    public class RobotsLogsController : Controller
    {
        private RobotContext db = new RobotContext();

        // GET: RobotsLogs
        public ActionResult AddLog(int? id)
        {
            Robot robot = db.Robots.Where(r => r.RobId == id).FirstOrDefault();
            RobotsLog robLogObj = new RobotsLog { TimeStamp = DateTime.Now, RobId = id };

            if (robot != null)
            {
                robot.MailSended = false;
                db.RobotsLogs.Add(robLogObj);
                db.SaveChanges();
            }

            return View(db.RobotsLogs.OrderByDescending(r => r.TimeStamp));
        }

        public ActionResult TimeManager()
        {
            List<RobotsLog> lateLogs = new List<RobotsLog>();
            Robot robot = new Robot();
            RobotsLog Log;

            foreach (Robot item in db.Robots)
            {
                Log = db.RobotsLogs
                     .Where(i => i.RobId == item.RobId)
                     .OrderByDescending(i => i.TimeStamp)
                     .FirstOrDefault();

                if (Log == null)
                {
                    lateLogs.Add(new RobotsLog { RobId = item.RobId, Rob = item });
                }

                else if (DateTime.Now - Log.TimeStamp > TimeSpan.FromMinutes(item.IntPuls))
                {
                    lateLogs.Add(Log);
                }
            }
            return View(lateLogs);
        }


        public void LateLogSendEmail()
        {
            RobotsLog Log;
            var robotList = db.Robots.ToList();

            

            foreach (Robot item in robotList)
            {
                Log = db.RobotsLogs
                     .Where(i => i.RobId == item.RobId)
                     .OrderByDescending(i => i.TimeStamp)
                     .FirstOrDefault();

                if (DateTime.Now - Log.TimeStamp > TimeSpan.FromMinutes(item.IntPuls))
                {
                    if (item.MailSended == false)
                    {
                        SendEmail(item, item.Beskrivning + " " + "Senaste skickad logg" + " " + Log.TimeStamp);
                        if (ModelState.IsValid)
                        {
                            Robot robot = db.Robots.Where(r => r.RobId == item.RobId).FirstOrDefault();
                            robot.MailSended = true;
                            db.Entry(item).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }

                }

            }

        }

        public void SendEmail(Robot robot, string emailbody)
        {
            RobotsLog robotsLog = new RobotsLog();

            if (robot.ContactInfo != null)
            {
                MailMessage mail = new MailMessage("rezazaman7@gmail.com", robot.ContactInfo);
                mail.Subject = "Loggen är inte mottagen";
                mail.Body = emailbody;

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.Credentials = new NetworkCredential("rezazaman7@gmail.com", "");
                smtpClient.EnableSsl = true;
                smtpClient.Send(mail);
            }
        }


        public ActionResult AddLogSearch(string beskriv)
        {

            var robs = db.Robots.Where(r => r.Beskrivning.Contains(beskriv)).FirstOrDefault();
            var logs = db.RobotsLogs.Where(r => r.RobId == robs.RobId);

            if (!string.IsNullOrWhiteSpace(beskriv) && db.Robots.Where(r => r.Beskrivning.Contains(beskriv)).Any())
            {
                return View("AddLog", logs);
            }

            return View("AddLog", db.RobotsLogs.OrderByDescending(r => r.TimeStamp));

        }

        // GET: RobotsLogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RobotsLog robotsLog = db.RobotsLogs.Find(id);
            if (robotsLog == null)
            {
                return HttpNotFound();
            }
            return View(robotsLog);
        }

        // GET: RobotsLogs/Create
        public ActionResult Create()
        {
            ViewBag.RobId = new SelectList(db.Robots, "RobId", "Beskrivning");
            return View();
        }

        // POST: RobotsLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TimeStamp,RobId")] RobotsLog robotsLog)
        {
            if (ModelState.IsValid)
            {
                db.RobotsLogs.Add(robotsLog);
                db.SaveChanges();
                return RedirectToAction("AddLog");
            }

            ViewBag.RobId = new SelectList(db.Robots, "RobId", "Beskrivning", robotsLog.RobId);
            return View(robotsLog);
        }

        // GET: RobotsLogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RobotsLog robotsLog = db.RobotsLogs.Find(id);
            if (robotsLog == null)
            {
                return HttpNotFound();
            }
            ViewBag.RobId = new SelectList(db.Robots, "RobId", "Beskrivning", robotsLog.RobId);
            return View(robotsLog);
        }

        // POST: RobotsLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TimeStamp,RobId")] RobotsLog robotsLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(robotsLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RobId = new SelectList(db.Robots, "RobId", "Beskrivning", robotsLog.RobId);
            return View(robotsLog);
        }

        // GET: RobotsLogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RobotsLog robotsLog = db.RobotsLogs.Find(id);
            if (robotsLog == null)
            {
                return HttpNotFound();
            }
            return View(robotsLog);
        }

        // POST: RobotsLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RobotsLog robotsLog = db.RobotsLogs.Find(id);
            db.RobotsLogs.Remove(robotsLog);
            db.SaveChanges();
            return RedirectToAction("AddLog");
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
