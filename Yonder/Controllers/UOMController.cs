using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonder.Models;

namespace Yonder.Controllers
{
    public class UOMController : Controller
    {
        // GET: UOM
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddUOM(UOM c)
        {
            bool Success = false;
            string Message = "";
            if (c.AddUOM())
            {
                Success = true;
                Message = "UOM Added Successfully";
            }
            else
            {
                Success = false;
                Message = "UOM Adding failed";
            }
            return Json(new { Success = Success, Message = Message });
        }

        public ActionResult getUOMs()
        {
            UOM c = new UOM();
            return Json(c.getUOMs(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(string id)
        {
            bool Success = false;
            UOM c = new UOM();
            if (c.Delete(id))
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