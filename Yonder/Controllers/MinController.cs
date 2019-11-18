using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yonder.Models;
using Yonder.ViewModels;

namespace Yonder.Controllers
{
    public class MinController : Controller
    {
        // GET: Min
        public ActionResult Index()
        {
            Shop s = new Shop();
            MaterialInwordViewModel mivm = new MaterialInwordViewModel()
            {
                Material_Inword = new Material_In(),
                Material_Inword_items = new Material_In_Items(),
                ShopList = s.getShops()
            };
            return View(mivm);
        }

        [HttpPost]
        public ActionResult Add(MaterialInwordViewModel mivm)
        {
            bool success = false;
            string message = "";
            int miNumber = 0;
            if (mivm.Material_Inword.mi_number != 0)
            {
                if (mivm.AddItem(mivm.Material_Inword.mi_number))
                {
                    miNumber = mivm.Material_Inword.mi_number;
                    success = true;
                    message = "Inword Item Added Successfully";
                }
                else
                {
                    success = false;
                    message = "Inword Item Adding Failed";
                }
            }
            else 
            {
                mivm.Material_Inword.InwordDate = DateTime.Now;
                mivm.Material_Inword.InwordTime = DateTime.Now.ToShortTimeString();
                
                if (mivm.Save(ref miNumber))
                {
                    success = true;
                    message = "Inword Item Added Successfully";
                }
                else
                {
                    success = false;
                    message = "Inword Item Adding Failed";
                }
            }
            return Json(new { success = success, message = message, data = new { miNumber=miNumber} }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getMatInItems(int id)
        {
            Material_In_Items mi = new Material_In_Items();
            return Json(mi.getMaterialInItems(id),JsonRequestBehavior.AllowGet);
        }

        public ActionResult List()
        {
            Material_In m = new Material_In();
            return View(m.getMaterialIn());
        }

        public ActionResult View(int id)
        {
            Shop s = new Shop();
            Material_In m = new Material_In();
            m = m.getMaterialIn(id);
            MaterialInwordViewModel mivm = new MaterialInwordViewModel() 
            {
                Material_Inword=m,
                Material_Inword_items = new Material_In_Items(),
                ShopList = s.getShops()
            };
            return View("Index",mivm);
        }

        public ActionResult RemoveItem(int id)
        {
            bool success = false;
            Material_In_Items mii = new Material_In_Items();
            if (mii.removeMaterialInItem(id))
            {
                success = true;
            }
            else
            {
                success = false;
            }
            return Json(new { success = success}, JsonRequestBehavior.AllowGet);
        }
    }
}