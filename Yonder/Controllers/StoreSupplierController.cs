using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonder.Models;

namespace Yonder.Controllers
{
    public class StoreSupplierController : Controller
    {
        // GET: StoreSupplier
        public ActionResult Index()
        {
            StoreSupplier s = new StoreSupplier();
            return View(s.GetSuppliers());
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(StoreSupplier s)
        {
            bool success = false;
            string message = "";
            if(s.Supplier_Id==0)
            {
                if (s.AddSupplier())
                {
                    success = true;
                    message = "Supplier Added Successfully";
                }
                else
                {
                    success = false;
                    message = "Supplier Adding Failed Details Might be duplicated";
                }
            }
            else
            {
                if (s.UpdateSupplier())
                {
                    success = true;
                    message = "Supplier Updated Successfully";
                }
                else
                {
                    success = false;
                    message = "Supplier Updation Failed Details Might be duplicated";
                }
            }
          
            return Json(new { success=success,message=message},JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(decimal id)
        {
            StoreSupplier s = new StoreSupplier();
            s = s.GetSuppliers()
                .Where(x => x.Supplier_Id == id).ToList()[0];
            return View("New",s);
        }

        public ActionResult Delete(int id)
        {
            bool success = false;
            string message = "";
            StoreSupplier s=new StoreSupplier();
            if (s.DeleteSupplier(id))
            {
                success = true;
                message = "Supplier Delete Successfully";
            }
            else
            {
                success = false;
                message="Supplier Deletion Failed";
            }
            return Json(new { success = success, message = message }, JsonRequestBehavior.AllowGet);
        }
    }
}