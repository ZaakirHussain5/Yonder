using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonder.Models;

namespace Yonder.Controllers
{
    public class ShopHomeController : Controller
    {
        // GET: ShopHome
        public ActionResult Index()
        {
            if (Session["Shop"] == null)
                return RedirectToAction("Logout", "AdminHome");
            return View((Shop)Session["Shop"]);
        }

        public ActionResult GetShopName()
        {
            return Content(((Shop)Session["Shop"]).Owner);
        }
    }
}