using Capstone_Project.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone_Project.Controllers
{
    public class HomeController : Controller
    {
        private CapstoneFinal20180412084401_dbEntities db = new CapstoneFinal20180412084401_dbEntities();

        public ActionResult Register()
        {
            ViewBag.test = "......";
            return View();
        }

        [HttpPost]
     
        public ActionResult Register(Login log)
        {
            
            if (ModelState.IsValid)
            {
                var username = from l in db.Logins
                               where l.UserName == log.UserName
                               select l;
                if (log.Password.Equals(log.ConfirmPassword) && username.FirstOrDefault() == null)
                {
                    db.Logins.Add(log);
                    db.SaveChanges();
                    return RedirectToAction("Signup");
                }
                else
                {
                    return RedirectToAction("Register");    
                }
                    
            }
            return RedirectToAction("Register");

        }

        public ActionResult Signup()
        {
            var login = from l in db.Logins
                        orderby l.Login_ID descending
                        select l;
            Login logForId = login.First();
            int firstId = logForId.Login_ID;
            ViewBag.lastId = firstId;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup([Bind(Include = "PKTalent_ID,FirstName,LastName,Email,HomePhoneNum,CellPhoneNum,BirthDate,Gender,Weight,Height,EyeColor,HairColor,UnionStatus,SIN,LoginLogin_ID,NumOfRequest,NumOfHired,EthinicOrigin,CarMake,CarModel,CarYear,CarColor")] Talent talent)
        {

            if (ModelState.IsValid)
            {              
                db.Talents.Add(talent);
                db.SaveChanges();
                return RedirectToAction("Login");

            }

            return RedirectToAction("Signup");
        }

        public ActionResult Login()
        {
            return View();
        }

        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login log)
        {
            if (ModelState.IsValid)
            {
                var login = from l in db.Logins
                            where l.UserName == log.UserName && l.Password == log.Password
                            select l;

                if (login.FirstOrDefault() != null)
                {
                    foreach (Login item in login)
                    {
                        var queryForTalent = from l in db.Talents
                                             where l.LoginLogin_ID == item.Login_ID
                                             select l;
                        if (queryForTalent.FirstOrDefault() != null)
                        {
                                foreach (Talent talent in queryForTalent)
                                {
                                     TempData["Talent"] = talent;
                                     Session["TalentId"] = talent.LoginLogin_ID;
                                     return RedirectToAction("WelcomeTalent");
                                 }
                           
                        }

                        var queryForAdmin = from l in db.Admins
                                            where l.LoginLogin_ID == item.Login_ID
                                            select l;
                        if (queryForAdmin.FirstOrDefault() != null)
                        {
                            foreach (Admin admin in queryForAdmin)
                            {
                                TempData["Admin"] = admin;
                                Session["AdminId"] = admin.LoginLogin_ID;
                            }
                            return RedirectToAction("WelcomeAdmin");
                        }

                        var queryForCastingDirector = from l in db.CastingDirectors
                                                      where l.LoginLogin_ID == item.Login_ID
                                                      select l;
                        if (queryForCastingDirector.FirstOrDefault() != null)
                        {
                            foreach (CastingDirector cd in queryForCastingDirector)
                            {
                                TempData["CastingDirector"] = cd;
                                Session["CastingDirectorId"] = cd.LoginLogin_ID;
                            }
                            return RedirectToAction("WelcomeCastingDirector");
                        }
                    }
                }

                else
                {

                }
            }
            return View(log);
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewAllCastingDirector()
        {
            return View();
        }

        public ActionResult WelcomeTalent()
        {
            if (Session["TalentId"] != null)
                return View(TempData["Talent"]);
            else
                return View("Login");
        }

        public ActionResult WelcomeAdmin()
        {
            if (Session["AdminId"] != null)
                return View(TempData["Admin"]);
             else
                return View("Login");
        }

        public ActionResult WelcomeCastingDirector()
        {
            if (Session["CastingDirectorId"] != null)
                return View(TempData["CastingDirectors"]);
            else
                return View("Login");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login","Home");
        }

        public ActionResult ChangeUsername()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult SearchTalent()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}