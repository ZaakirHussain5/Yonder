using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonder.Models;

namespace Yonder.Controllers
{
    public class ShopProfileController : Controller
    {
        // GET: ShopProfile
        public ActionResult Index()
        {
            if (Session["Shop"] == null)
                return RedirectToAction("Logout", "AdminHome");

            return View((Shop)Session["Shop"]);
        }

        [HttpPost]
        public ActionResult Save(Shop s)
        {
            bool Success = false;
            string Message = "";
            if (s.UpdateDetails())
            {
                Success = true;
                Message = "Details Updated Successfully";
            }
            else
            {
                Success = false;
                Message = "Details Updation Failed";
            }
            return Json(new {Success=Success,Message=Message },JsonRequestBehavior.AllowGet);
        }
    }
}