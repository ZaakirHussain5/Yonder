using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonder.Models;

namespace Yonder.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            if (Session["Shop"] == null)
                return RedirectToAction("Logout", "AdminHome");
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category c)
        {
            bool Success = false;
            string Message = "";
            Shop s = new Shop();
            s = (Shop)Session["Shop"];
            c.ShopNumber = s.ShopNumber;
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
            Category c = new Category();
            Shop s = new Shop();
            s = (Shop)Session["Shop"];
            c.categoriesList = c.getCategories(s.ShopNumber);
            return Json( c.categoriesList,JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            bool Success = false;
            Category c = new Category();
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