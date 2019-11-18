using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonder.Models;
using Yonder.ViewModels;

namespace Yonder.Controllers
{
    public class VendorPaymentsController : Controller
    {
        // GET: VendorPayments
        public ActionResult Index()
        {
            VendorPaymentsViewModel vpvm = new VendorPaymentsViewModel();
            return View(vpvm.getVendorPayments());
        }

        [Route("VendorPayments/Details/{Shopnumber}")]
        public ActionResult DetailsLevelOne(int shopnumber)
        {
            VendorPayments vp = new VendorPayments();
            return View(vp.getVendorPayments_unpaid(shopnumber));
        }

        [Route("VendorPayments/Details/{Shopnumber}/{billdate}")]
        public ActionResult DetailsLevelTwo(int shopnumber,string billdate)
        {
            BillingMain bm = new BillingMain();
            List<BillingMain> result = bm.getBills_billeddate(shopnumber, DateTime.Parse(billdate).ToString("yyyy-MM-dd"));
            TempData["Message"] = "";
            return result.Count != 0 ? View(result) :
            View("DetailsLevelOne",new VendorPayments().getVendorPayments_unpaid(shopnumber)); 
        }

        public ActionResult GetTotalCollection(int shopNumber)
        {
            BillingMain bm = new BillingMain();
            List<PaymentViewModel> totalcollection = bm.getTotalCollection(shopNumber);
            return Json(totalcollection,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Pay(List<VendorPayments> payments)
        {
            bool success = false;
            string message = "";
            try 
            {
                foreach (var payment in payments)
                {
                    payment.paid_date = DateTime.Now;
                    if (payment.UpdatePayment())
                        success = true;
                    else
                        throw new Exception();
                }
                message = "Payments Added Successfully";
            }
            catch
            {
                success = false;
                message = "Payments Adding failed";

            }
            return Json(new { success=success,message=message},JsonRequestBehavior.AllowGet);
        }
    }
}