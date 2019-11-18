using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonder.Models;

namespace Yonder.Controllers
{
    public class RefundController : Controller
    {
        // GET: Refund
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Refund(Refund r)
        {
            bool success = false;
            string message = "";
            r.refundDate = DateTime.Now;
            r.refundTime = DateTime.Now.ToShortTimeString();
            if (r.AddRefundRequest())
            {
                success = true;
                message = "Request Added Successfully";
            }
            else
            {
                success = false;
                message = "Refund request Failed";
            }
            return Json(new { success=success,message=message},JsonRequestBehavior.AllowGet);
        }
    }
}