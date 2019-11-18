using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonder.Models;

namespace Yonder.Controllers
{
    public class StoreHomeController : Controller
    {
        // GET: StoreHome
        public ActionResult Index()
        {
            if (Session["Store"] == null)
                return RedirectToAction("Index", "Home");
            return View();
        }

        public ActionResult GetStoreName()
        {
            return Content(((Shop)Session["Store"]).Owner);
        }

        public ActionResult Profile()
        {

            return View((Shop)Session["Store"]);
        }
    }
}