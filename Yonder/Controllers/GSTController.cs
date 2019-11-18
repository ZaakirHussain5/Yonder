using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonder.Models;

namespace Yonder.Controllers
{
    public class GSTController : Controller
    {
        // GET: GST
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddGST(GST g)
        {
            bool Success = false;
            string Message = "";
            if (g.AddGST())
            {
                Success = true;
                Message = "GST Added Successfully";
            }
            else
            {
                Success = false;
                Message = "GST Adding failed";
            }
            return Json(new { Success = Success, Message = Message });
        }

        public ActionResult getGSTs()
        {
            GST g = new GST();
            return Json(g.getGSTs(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(decimal id)
        {
            bool Success = false;
            GST g = new GST();
            if (g.Delete(id))
            {
                Success = true;
            }
            else
            {
                Success = false;
            }
            return Json(new { Success = Success });
        }
    }
}