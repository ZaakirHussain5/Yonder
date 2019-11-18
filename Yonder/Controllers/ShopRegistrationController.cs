using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonder.Models;

namespace Yonder.Controllers
{
    public class ShopRegistrationController : Controller
    {
        // GET: ShopRegistration
        public ActionResult Index()
        {
            Shop s = new Shop();
            s.shopCategories=new ShopCategory().getCategories();
            return View(s);
        }

        [HttpPost]
        public ActionResult Save(Shop shop)
        {
            bool Success=false;
            string Message="";
            try
            {
                if (shop.CommenceDate != null)
                {
                    shop.Policy_Exp_Date = DateTime.Parse(shop.CommenceDate).AddMonths(12).ToShortDateString();
                }
                shop.isActive = true;
                if (shop.Save())
                {
                    Success = true;
                    Message = "Shop Registered Successfully";
                }
                else
                {
                    Success = false;
                    Message = "Shop Details might be Duplicated";
                }
            }
            catch
            {
                Success = false;
                Message = "Something Unexpected Happened";
            }
            return Json(new { Success = Success, Message = Message ,data=shop}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getShops()
        {
            return Json(new Shop().getShops(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int id)
        {
            Shop s = new Shop();
            s=s.getShopById(id);
            s.shopCategories = new ShopCategory().getCategories();
            return View("Index",s);
        }
    }
}