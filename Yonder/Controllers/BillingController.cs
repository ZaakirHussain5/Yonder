using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonder.Models;
using Yonder.ViewModels;

namespace Yonder.Controllers
{
    public class BillingController : Controller
    {
        // GET: Billing
        public ActionResult Index()
        {
            if (Session["Shop"] == null)
                return RedirectToAction("Logout", "AdminHome");
            BillingViewModel bvm = new BillingViewModel() 
            {
                billItems = new List<billItem>()
            };
            return View(bvm);
        }

        public ActionResult Manage(string fromDate = null, string toDate = null)
        {
            if (Session["Shop"] == null)
                return RedirectToAction("Logout", "AdminHome");
            BillingMain bm = new BillingMain();
            List<BillingMain> bills = new List<BillingMain>();
            if (string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
            {
                bills = bm.getBills(((Shop)Session["Shop"]).ShopNumber);
            }
            else
            {
                bills = bm.getBills(fromDate,toDate,((Shop)Session["Shop"]).ShopNumber);
            }
            return View(bills);
        }

        [HttpPost]
        public ActionResult ItemSearch(string query)
        {
            Menu menu = new Menu();
            List<Menu> MenuList = menu.GetShopMenu(((Shop)Session["Shop"]).ShopNumber);

            if (!String.IsNullOrWhiteSpace(query))
                MenuList = MenuList.Where(m => m.itemName.ToLower().Contains(query)).ToList();

            return Json(MenuList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(string invNumber)
        {
            if(string.IsNullOrEmpty(invNumber))
                return RedirectToAction("Details");
            BillingMain bm = new BillingMain();
            bm = bm.getBillInfo(((Shop)Session["Shop"]).ShopNumber,invNumber);
            return View(bm);
        }

        [HttpPost]
        public ActionResult Bill(BillingViewModel bvm)
        {
            bool success = false;
            string message = "";
            try
            {
                if (checkBalance(bvm))
                {
                    string invNumber = "";
                    if (bvm.GenerateBill(ref invNumber,(Shop)Session["Shop"]))
                    {
                        success = true;
                        message = "Bill Generated Successfully Invoice Number is " + invNumber;
                    }
                    else
                    {
                        success = false;
                        message = "Bill Generation Failed" ;
                    }
                }
                else
                {
                    success = false;
                    message = "Insufficient Balance to make the Payment";
                }
            }
            catch
            {
                success = false;
                message = "Something Unexpected happened";
            }
            return Json(new { success = success, message = message });
        }

        public ActionResult CancelBill(string invNumber)
        {
            bool success = false;
            string message = "";
            BillingMain bm=new BillingMain();
            bm = bm.getBillInfo(((Shop)Session["Shop"]).ShopNumber, invNumber);
            if (bm.UpdateBillStatus("Cancelled"))
            {
                success = true;
                message = "Bill Cancelled Successfully";
            }
            else
            {
                success = false;
                message = "Bill Cancellation Failed";
            }
            return Json(new { success=success,message=message },JsonRequestBehavior.AllowGet);
        }

        public bool checkBalance(BillingViewModel bvm)
        {
            CardRegistration card = new CardRegistration();
            Dictionary<string, decimal> result = bvm.caluclations(card.getCardInfo(bvm.cardNumber));
            return (( 
                result["totalAfterDiscount"]+ result["taxVal"]) < 
                (result["cardBalance"] - result["minBal"]));
        }
    }
}