using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.SessionState;
using System.Web;
using System.Web.Mvc;
using LibraryTest.Models;

namespace LibraryTest.Controllers
{

    public class AccountController : Controller
    {

        public ActionResult Register()
        {
            return View();
        }

        //Add new user to database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(AccountViewModel account)
        {
            if (ModelState.IsValid)
            {
                //Users database working
                UsersDBWorking userDB = new UsersDBWorking();

                userDB.AddNewUserToDB(account);
                ModelState.Clear();
            }
            return View();
        }


        public ActionResult LogIn()
        {
            if (Session["UserMail"] != null)
                return RedirectToAction("Index", "Home");
            else
                return View();
        }

        // Get user email from database and compare with input email
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(AccountViewModel account)
        {
            UsersDBWorking dataBase = new UsersDBWorking();
            if (dataBase.GetUserEmailFromDB(account) != "null")
            {
                Session["UserMail"] = account.Email;
                Session["AdminPass"] = "Admin456";
                string sesId = Session.SessionID;

                return RedirectToAction("LoggedIn");
            }
            else
            {
                ModelState.AddModelError("", "Email address is wrong.");
            }
            return View();
        }

        // Redirecting to take book
        public ActionResult LoggedIn()
        {
            if (Session["UserMail"] != null)
            {
                return RedirectToAction("TakeBook", "Book");
            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }

        public ActionResult LogOut()
        {
            Session["UserMail"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AdminPanel()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminPanel(AdminPanelPass admin)
        {
            if (ModelState.IsValid)
            {
                if (admin.Password == Session["AdminPass"].ToString())
                {
                    ModelState.Clear();
                    return RedirectToAction("ManageBook", "Book");
                }
                else
                    return RedirectToAction("TakeBook", "Book");
            }
            else return RedirectToAction("TakeBook", "Book");
        }


    }
}