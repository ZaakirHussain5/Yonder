using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonder.Models;

namespace Yonder.Controllers
{
    public class StoreInventoryController : Controller
    {
        // GET: StoreInvertory
        public ActionResult Index()
        {
            StoreInventory si = new StoreInventory();
            return View(si.getInventory());
        }

        public ActionResult New()
        {
            StoreCategory s = new StoreCategory();
            GST g = new GST();
            UOM u = new UOM();
            return View(new StoreInventory() 
            {
                StoreGSTs=g.getGSTs(),
                StoreUOMs=u.getUOMs(),
                StoreCategories=s.getCategories(),
                status=0
            });
        }

        [HttpPost]
        public ActionResult Save(StoreInventory si)
        {
            bool success = false;
            string message = "";
            if (si.status == 0)
            {
                if (si.AddInventory())
                {
                    success = true;
                    message = "Item Added Successfully";
                }
                else
                {
                    success = false;
                    message = "Item Adding Failed Details Might be duplicated";
                }
            }
            else
            {
                if (si.UpdateInvenory())
                {
                    success = true;
                    message = "Item Updated Successfully";
                }
                else
                {
                    success = false;
                    message = "Item Updation Failed Details Might be duplicated";
                }
            }

            return Json(new { success = success, message = message }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(string id)
        {
            StoreInventory si = new StoreInventory();
            si = si.getInventory_ItemCode(id);
            si.status = 1;
            si.StoreCategories = new StoreCategory().getCategories();
            si.StoreGSTs = new GST().getGSTs();
            si.StoreUOMs = new UOM().getUOMs();
            return View("New",si);

        }
    }
}