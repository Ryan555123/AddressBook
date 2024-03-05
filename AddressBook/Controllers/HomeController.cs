using AddressBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AddressBook.Controllers
{
    public class HomeController : Controller
    {
        AddressBookDB db = new AddressBookDB();

        public ActionResult Index(string Type , string user_input)
        {
            //sort Dep category
            ViewData["Sort_Type"] = db.People.Select(x => x.Type).Distinct().ToList();

            var Person = db.People.OrderBy(x => x.Type).ThenBy(x => x.Dep).ThenBy(x => x.Name_CH).ToList();

            //By Type
            if (Type != null)
            {
                if (!Type.Equals("ALL"))
                {
                    Person = db.People.Where(x => x.Type.Equals(Type))
                                .OrderBy(x => x.Type).ThenBy(x => x.Dep).ThenBy(x => x.Name_CH).ToList();
                }
            }

            //By user_input
            if (user_input != null)
            {
                Person = db.People.Where(x => x.Name_CH.Contains(user_input) || x.Name_EN.Contains(user_input) || x.UserNo.Contains(user_input) || x.Extension_Num.Contains(user_input) || x.Dep.Contains(user_input))
                    .OrderBy(x => x.Type).ThenBy(x => x.Dep).ThenBy(x => x.Name_CH).ToList();
            }

            if (Person == null)
                return HttpNotFound();
            else
                return View(Person);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(string TxtAccount, string TxtPassword)
        {
            if(ModelState.IsValid)
            {
                var Account = new Account();
                if(Account.CheckAccount(TxtAccount, TxtPassword))
                {
                    var ticket = new FormsAuthenticationTicket(
                            version: 1,
                            name: "Admin",
                            issueDate: DateTime.UtcNow,
                            expiration: DateTime.UtcNow.AddMinutes(1),
                            isPersistent: true,
                            userData: "Admin", 
                            cookiePath: FormsAuthentication.FormsCookiePath
                        );

                    var encryptedTicket = FormsAuthentication.Encrypt(ticket); 
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(cookie);

                    return Json(new { loginStatus = true, message = "登入成功" , redirectUrl = Url.Action("Admin", "Home") });
                }
            }

            return Json(new { loginStatus = false, message = "無效的使用者名稱或密碼" });
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        public ActionResult Error(string SearchStr)
        {
            return View();
        }

        [Authorize]
        public ActionResult Admin()
        {
            return View();
        }

        [Authorize]
        public JsonResult AjaxData(string SearchStr)
        {
            if(SearchStr == null)
            {
                return Json(db.People.ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<Person> person = db.People
                    .Where(x => x.Name_CH.Contains(SearchStr) || x.Name_EN.Contains(SearchStr) || x.UserNo.Contains(SearchStr) || x.Extension_Num.Contains(SearchStr) || x.Dep.Contains(SearchStr) || x.Type.Contains(SearchStr)).ToList();

                return Json(person, JsonRequestBehavior.AllowGet);
            }
        }
    }
}