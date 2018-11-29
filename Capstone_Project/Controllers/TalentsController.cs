using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Capstone_Project.Models;

namespace Capstone_Project.Controllers
{
    public class TalentsController : Controller
    {
        private Capstone_Final_LatestEntities1 db = new Capstone_Final_LatestEntities1();

        // GET: Talents
        public ActionResult Index()
        {
            var talents = db.Talents.Include(t => t.Login);
            return View(talents.ToList());
        }

  

        // GET: Talents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Talent talent = db.Talents.Find(id);
            if (talent == null)
            {
                return HttpNotFound();
            }
            return View(talent);
        }

        // GET: Talents/Create
        public ActionResult Create()
        {
            //ViewBag.LoginLogin_ID = new SelectList(db.Logins, "Login_ID", "Login_ID");
            var login = from l in db.Logins
                        orderby l.Login_ID descending
                        select l;
            Login logForId = login.First();
            int firstId = logForId.Login_ID;
            ViewBag.lastId = firstId;

            return View();
        }

        // POST: Talents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PKTalent_ID,FirstName,LastName,Email,HomePhoneNum,CellPhoneNum,BirthDate,Gender,Weight,Height,EyeColor,HairColor,UnionStatus,SIN,LoginLogin_ID,NumOfRequest,NumOfHired,EthinicOrigin,CarMake,CarModel,CarYear,CarColor")] Talent talent)
        {
            
                if (ModelState.IsValid)
            {

               // db.Logins.Add((Login) Session["log"]);
                db.Talents.Add(talent);

                //Login log = db.Logins.FirstOrDefault(c=> c.UserName == "abeer");
                //db.Talents.Add(new Talent { UserName = "abeer", LoginLogin_ID = log.Login_ID });
                db.SaveChanges();
                //return RedirectToAction("Index");
                //return Redirect("https://bonkercastingwebsite.azurewebsites.net/Home/Login");
            }

            //ViewBag.LoginLogin_ID = new SelectList(db.Logins, "Login_ID", "Username", talent.LoginLogin_ID);
            //return View(talent);
            //return View("Home/Login");
            return Redirect("https://bonkercastingwebsite.azurewebsites.net/Home/Login");
        }

        // GET: Talents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Talent talent = db.Talents.Find(id);
            if (talent == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoginLogin_ID = new SelectList(db.Logins, "Login_ID", "UserName", talent.LoginLogin_ID);
            return View(talent);
        }

        // POST: Talents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PKTalent_ID,FirstName,LastName,Email,HomePhoneNum,CellPhoneNum,BirthDate,Gender,Weight,Height,EyeColor,HairColor,UnionStatus,SIN,LoginLogin_ID,NumOfRequest,NumOfHired")] Talent talent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(talent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoginLogin_ID = new SelectList(db.Logins, "Login_ID", "UserName", talent.LoginLogin_ID);
            return View(talent);
        }

        // GET: Talents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Talent talent = db.Talents.Find(id);
            if (talent == null)
            {
                return HttpNotFound();
            }
            return View(talent);
        }

        // POST: Talents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Talent talent = db.Talents.Find(id);
            db.Talents.Remove(talent);
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
