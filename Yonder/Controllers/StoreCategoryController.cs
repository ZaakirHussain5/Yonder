using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonder.Models;

namespace Yonder.Controllers
{
    public class StoreCategoryController : Controller
    {
        // GET: StoreCategory
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddCategory(StoreCategory c)
        {
            bool Success = false;
            string Message = "";
            if (c.AddCategory())
            {
                Success = true;
                Message = "category Added Successfully";
            }
            else
            {
                Success = false;
                Message = "category Adding failed";
            }
            return Json(new { Success = Success, Message = Message });
        }

        public ActionResult getCategories()
        {
            StoreCategory c = new StoreCategory();
            return Json( c.getCategories(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(string id)
        {
            bool Success = false;
            StoreCategory c = new StoreCategory();
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