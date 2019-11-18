using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonder.Models;

namespace Yonder.Controllers
{
    public class StorePurchaseController : Controller
    {
        // GET: StorePurchase
        [Route("Purchase")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SupplierSearch(string query)
        {
            StoreSupplier menu = new StoreSupplier();
            List<StoreSupplier> StoreSupplierList = menu.GetSuppliers();

            if (!String.IsNullOrWhiteSpace(query))
                StoreSupplierList = StoreSupplierList.Where(m => m.Name.ToLower().Contains(query)).ToList();

            return Json(StoreSupplierList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ItemSearch(string query)
        {
            StoreInventory inv = new StoreInventory();
            List<StoreInventory> StoreSupplierList = inv.getInventory();

            if (!String.IsNullOrWhiteSpace(query))
                StoreSupplierList = StoreSupplierList.Where(m => m.item.ToLower().Contains(query)).ToList();

            return Json(StoreSupplierList, JsonRequestBehavior.AllowGet);
        }
    }
}