using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonder.Models;

namespace Yonder.Controllers
{
    public class RechargeController : Controller
    {
        // GET: Recharge
        public ActionResult Index()
        {
            Recharge r = new Recharge();
            Package p = new Package();
            r.Packages = p.getPackages();
            return View(r);
        }

        public ActionResult getPlans(string id)
        {
            Package p = new Package();
            return Json(p.getPackages_cardType(id),JsonRequestBehavior.AllowGet);
        }

        public ActionResult getPlansDetails(int id)
        {
             Package p = new Package();
             return Json(p.getPackageByAmount(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Recharge(Recharge r)
        {
            bool success = false;
            string message = "";
            r.RechargeDate = DateTime.Now;
            r.RechargeTime = DateTime.Now.ToShortTimeString();
            CardRegistration c = new CardRegistration();
            c = c.getCardInfo(r.CardNumber);
            if (r.makeRecharge() &&
                c.UpdateBalanceCredit(r.Amount))
            {
                
                success = true;
                message = "Recharge Done";
            }
            else
            {
                success = false;
                message = "Recharge Failed";
            }
            return Json(new { success = success, message = message });
        }
    }
}