using Capstone_Project.Models;
using Microsoft.Reporting.WebForms;
using Rotativa;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Capstone_Project.Controllers
{
    public class HomeController : Controller
    {
        private Capstone_Final_LatestEntities1 db = new Capstone_Final_LatestEntities1();

        public ActionResult ViewAllTalents()
        {
            return View(db.Talents.ToList());
        }
        public ActionResult PrintViewToPdf()
        {
            var report = new ActionAsPdf("ViewAllTalents");
            return report;
        }

        //public ActionResult Reports(string ReportType)
        //{
        //    LocalReport localReport = new LocalReport();
        //    localReport.ReportPath = Server.MapPath("~/Reports/Report1.rdlc");

        //    ReportDataSource reportDataSource = new ReportDataSource();
        //    reportDataSource.Name = "TalentDataSet";
        //    reportDataSource.Value = db.Talents.ToList();
        //    localReport.DataSources.Add(reportDataSource);

        //    string reportType = ReportType;
        //    string mimeType;
        //    string encoding;
        //    string fileNameExtension;

        //    if (reportType == "PDF")
        //    {
        //        fileNameExtension = "pdf";
        //    }

        //    string[] streams;
        //    Warning[] warnings;
        //    byte[] renderedByte;

        //    renderedByte = localReport.Render(reportType, "", out mimeType, out encoding, out fileNameExtension,
        //        out streams, out warnings);
        //    Response.AddHeader("content-disposition", "attachment:filename=student_report." + fileNameExtension);

        //    return File(renderedByte, fileNameExtension);
        //}

        public ActionResult Register()
        {
            
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
                    if(log.Password != log.ConfirmPassword)
                    {
                        ViewBag.messageForPass = "Password and Confirm Password doesn't match";
                    }

                    else if(username.FirstOrDefault() != null)
                    {
                        ViewBag.messageForPass = "Username already exist.";
                    }

                    return View("Register");    
                }
                    
            }
            return View(log);

        }
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

        public ActionResult Signup()
        {
            var login = from l in db.Logins
                        orderby l.Login_ID descending
                        select l;
            Login logForId = login.First();
            int firstId = logForId.Login_ID;
            Session["lastId"] = firstId;
         
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup([Bind(Include = "PKTalent_ID,FirstName,LastName,Email,HomePhoneNum,CellPhoneNum,BirthDate,Gender,Weight,Height,EyeColor,HairColor,UnionStatus,SIN,LoginLogin_ID,EthinicOrigin,CarMake,CarModel,CarYear,CarColor,ImagePath,City,PostalCode")] Talent talent, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Img"), fileName);
                    file.SaveAs(path);
                    talent.ImagePath = file.FileName;
                    //talent.ImagePath = Url.Content("~/Img/" + fileName);

                }

                db.Talents.Add(talent);
                db.SaveChanges();
                ViewBag.Signup = "You have been successfully registered. Please click here to.";
                return View();
            }
            return View(talent);    

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
                                     Session["Talent"] = talent;
                                     Session["TalentId"] = talent.LoginLogin_ID;
                                     TempData["oldImagePath"] = talent.ImagePath;
                                     Session["LoginId"] = talent.LoginLogin_ID;
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
                                Session["CastingDirector"] = cd;
                                Session["CastingDirectorId"] = cd.LoginLogin_ID;
                                return RedirectToAction("WelcomeCastingDirector");
                            }
                            
                        }
                    }
                }

                else
                {
                    
                    ViewBag.Error = "Username/Password is not correct.";
                    
                }
            }
            return View(log);
        }
        // GET: Talents/Edit/5
        public ActionResult Edit(int? id)
        {
            //if (id == null)
            //{

            //    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //    return View();
            //}
            Talent talent = db.Talents.Find(id);
            //if (talent == null)
            //{
            //    //return HttpNotFound();
            //    return View();
            //}
           // ViewBag.LoginLogin_ID = new SelectList(db.Logins, "Login_ID", "UserName", talent.LoginLogin_ID);
            return View(talent);
        }

        //POST: Talents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PKTalent_ID,FirstName,LastName,Email,HomePhoneNum,CellPhoneNum,BirthDate,Gender,Weight,Height,EyeColor,HairColor,UnionStatus,SIN,LoginLogin_ID,EthinicOrigin,CarMake,CarModel,CarYear,CarColor,ImagePath,City,PostalCode")] Talent talent, HttpPostedFileBase file)
        {
        
            if (ModelState.IsValid)
            {
                //if (file != null && file.ContentLength > 0)
                //{
                //    string fileName = Path.GetFileName(file.FileName);
                //    string imagePath = Path.Combine(Server.MapPath("~/Img/"), fileName);
                //    file.SaveAs(imagePath);
                //    talent.ImagePath = Url.Content("~/Img/" + fileName);
                //}

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Img"), fileName);
                    talent.ImagePath = file.FileName;
                    file.SaveAs(path);
                    TempData["oldImagePath"] = talent.ImagePath;
                }

                if (talent.ImagePath == null)
                {
                    talent.ImagePath = TempData["oldImagePath"].ToString();
                }
                Session["Talent"] = talent;

                db.Entry(talent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("WelcomeTalent");
            }
            return View(talent);
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
            {
                return View(Session["Talent"]);
            }
            else
                return RedirectToAction("Login");
        }

        public ActionResult WelcomeAdmin()
        {
            if (Session["AdminId"] != null)
                return View(TempData["Admin"]);
            else
                return RedirectToAction("Login");
        }

        public ActionResult WelcomeCastingDirector()
        {
            if (Session["CastingDirectorId"] != null)
                return View(Session["CastingDirector"]);
            else
                return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login","Home");
        }

        public ActionResult ChangeUsername(int? id)
        {      
            Login log = db.Logins.Find(id);
     
            return View(log);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeUsername([Bind(Include = "Login_ID,UserName,Password,ConfirmPassword")] Login log)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(log).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Changeusername = "Your Username has been changed succesfully. Please click here to";
                //return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult ChangePassword(int? id)
        {
            Login log = db.Logins.Find(id);

            return View(log);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword([Bind(Include = "Login_ID,UserName,Password,ConfirmPassword")] Login log)

        {
            if (ModelState.IsValid)
            {
                if (log.Password != log.ConfirmPassword)
                {
                    ViewBag.PassMatch = "Password and Confirm Password doesn't match";
                    return View();
                }

                else
                {
                    ViewBag.Pass = "Your Password has been changed succesfully. Please click here to";
                    db.Entry(log).State = EntityState.Modified;
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                }
               
            }

            return View();
        }

      
        public ActionResult Search(string searchString, string searchString1, string searchString2, string searchString3, string searchString4, string searchString5, string searchString6, string searchString7)
        {

            var talents = from m in db.Talents
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                talents = talents.Where(s => s.FirstName.Contains(searchString) || s.LastName.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(searchString2))
            {
                talents = talents.Where(s => s.Gender.Contains(searchString2));

            }
            if (!String.IsNullOrEmpty(searchString3))
            {
                talents = talents.Where(s => s.EthinicOrigin.Contains(searchString3));

            }
            if (!String.IsNullOrEmpty(searchString4))
            {
                talents = talents.Where(s => s.Height.ToString().Contains(searchString4));

            }
            if (!String.IsNullOrEmpty(searchString5))
            {
                talents = talents.Where(s => s.Weight.ToString().Contains(searchString5));

            }
            if (!String.IsNullOrEmpty(searchString6))
            {
                talents = talents.Where(s => s.CarModel.Contains(searchString6));

            }
            if (!String.IsNullOrEmpty(searchString7))
            {
                talents = talents.Where(s => s.UnionStatus.Contains(searchString7));

            }
            return View(talents);
        }


        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode)
        {

            var verifyUrl = "/Home/ResetPassword/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("krish.bhagria96@gmail.com", "Bonker Casting");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "Krishsra3792";
            string subject = "Password Reset";

            string body = "<br/><br/>Click on below link to reset your password" +
                "<br/><br/><a href='" + link + "'>" + link + "</a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })

                smtp.Send(message);
        }


        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string EmailID)
        {
            string message = "";
            bool status = false;


            var account = db.Talents.Where(a => a.Email == EmailID).FirstOrDefault();

            if (account != null)
            {
                string resetCode = Guid.NewGuid().ToString();
                SendVerificationLinkEmail(account.Email, resetCode);
                account.ResetPaswordCode = resetCode;

                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                message = "Password reset link has been sent to your email address.";
                ViewBag.CodeSent = message;
            }

            else
            {
                if (EmailID.Equals(""))
                {
                    message = "Field can't be empty";
                }
                else
                {
                    message = "Email address not found";
                }
                ViewBag.CodeSent = message;
               
            }

            return View();
        }

        public ActionResult ResetPassword(string id)
        {
            var user = db.Talents.Where(a => a.ResetPaswordCode == id).FirstOrDefault();

            if (user != null)
            {
                ResetPasswordModel model = new ResetPasswordModel();
                model.ResetCode = id;
                return View(model);
            }

            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            
            

            if (ModelState.IsValid)
            {
                var user = db.Talents.Where(a => a.ResetPaswordCode == model.ResetCode).FirstOrDefault();
                if (user != null)
                {
                    var log = db.Logins.Where(a => a.Login_ID == user.LoginLogin_ID).FirstOrDefault();
                    log.Password = model.NewPassword;
                    user.ResetPaswordCode = "";
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    ViewBag.Message = "Your password has been changed succesfully. Please click here to";
                }
            }

            else
            {
                
            }

            return View(model);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

     

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}