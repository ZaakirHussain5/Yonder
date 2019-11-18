using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonder.Models;

namespace Yonder.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult UpdateProfile()
        {
            if (Session["User"] == null)
                return RedirectToAction("Index","Home");
            CardRegistration card = new CardRegistration();
            card = card.getCardInfo(Session["User"].ToString());
            return View(card);
        }

        [HttpPost]
        public ActionResult UpdateProfile(CardRegistration card)
        {
           bool success = false;
            string Message = "";
            try
            {
                card.CardNumber = Session["User"].ToString();
                if (card.updateProfile())
                {
                    success = true;
                    Message = "Profile Updated Successfully";
                }
                else 
                {
                    success = false;
                    Message = "Profile Updation Failed";
                }
            }
            catch
            {
                success = false;
                Message = "Something Unexpected Happened";
            }
            return Json(new { success = success, Message = Message });
        }

        [HttpPost]
        public ActionResult UpdatePin(string pin)
        {
            bool success = false;
            string Message = "";
            try
            {
                CardRegistration card = new CardRegistration();
                card = card.getCardInfo(Session["User"].ToString());
                if (card.PIN == pin)
                {
                    if (card.updatePin(pin, Session["User"].ToString()))
                    {
                        success = true;
                        Message = "Pin Updated Successfully";
                    }
                    else
                    {
                        success = false;
                        Message = "Pin Updation Failed";
                    }
                }
                else
                {
                    success = false;
                    Message = "Invalid Old PIN";
                }
            }
            catch
            {
                success = false;
                Message = "Something Unexpected Happened";
            }
            return Json(new { success = success, Message = Message });
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Profile()
        {
            return Content("30");
        }
    }
}