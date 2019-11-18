using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonder.Models;
using Yonder.ViewModels;

namespace Yonder.Controllers
{
    public class UserTransactionController : Controller
    {
        // GET: UserTransaction
        public ActionResult Index()
        {
            if (Session["User"] == null)
                return RedirectToAction("Index", "Home");
            CardRegistration user = new CardRegistration();
            user = user.getCardInfo(Session["User"].ToString());
            return View(new UserTransactionViewModel()
            {
                User = user
            }
            );
        }

        [Route("UserTransaction/{FromDate}/{ToDate}")]
        public ActionResult Index(string FromDate, string ToDate)
        {
            if (Session["User"] == null)
                return RedirectToAction("Index", "Home");
            CardRegistration user = new CardRegistration();
            user=user.getCardInfo(Session["User"].ToString());
            return View(new UserTransactionViewModel()
            {
                User = user,
                UserTransactions=new UserTransaction().getUserTransactionsBetweenDates(
                user.CardNumber,FromDate,ToDate)
            });
        }

        public ActionResult GetPackages(string Type = "Basic")
        {
            CardRegistration user = new CardRegistration();
            user = user.getCardInfo(Session["User"].ToString());
            Package p = new Package();
            return Json(p.getPackages_cardType(user.CardType)
                .Where(x=>x.Type==Type).ToList(),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Recharge(Recharge r)
        {
            bool success = false;
            string message = "";
            r.RechargeDate = DateTime.Now;
            r.RechargeTime = DateTime.Now.ToShortTimeString();
            CardRegistration c = new CardRegistration();
            c = c.getCardInfo(Session["User"].ToString());
            r.CardNumber = c.CardNumber;
            r.RechargedBy = "User";
            r.CardType = c.CardType;
            r.mop = "Online";
           
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