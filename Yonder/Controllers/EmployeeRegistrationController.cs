using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonder.Models;



namespace Yonder.Controllers
{
    public class EmployeeRegistrationController : Controller
    {
        // GET: EmployeeRegistration
        public ActionResult Index()
        {
            CardRegistration card = new CardRegistration();
            return View(card);
        }

        public ActionResult validateCard(string id)
        {
            CardRegistration card = new CardRegistration();
            if (card.check(id))
            {
                card = card.getCardInfo(id);
            }
            return Json(new { isValid = card.check(id),data=card },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Save(CardRegistration card)
        {
            bool Success = false;
            string Message = "";
            try
            {
                card.userType = "Corporate";
                if (card.Save())
                {
                    Success = true;
                    Message = "User Registered Successfully";
                }
                else
                {
                    Success = false;
                    Message = "User Registeration Failed";
                }
 
            }
            catch
            {
                Success = false;
                Message = "Something Unexpected Happened";
            }
            return Json(new { Success = Success, Message = Message });
        }
    }
}